using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyAPI.Data;
using MyAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSetting _appSettings;

        public UsersController(MyDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Validate(LoginModel model)
        {
            var user = _context.Taikhoans.SingleOrDefault(
                p => p.TenTaiKhoan == model.TenTaiKhoan && p.MatKhau == model.MatKhau);

            if(user == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Sai TenTaiKhoan/MatKhau",
                });
            }

            //cấp token
            var token = await GenerateToken(user);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Đăng nhập thành công",
                Data = token
            });
        }
        private void CreatePasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private async Task<TokenModel> GenerateToken(Taikhoan taikhoan)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Tên tài khoản:",taikhoan.TenTaiKhoan),
                    new Claim(JwtRegisteredClaimNames.Email,taikhoan.Email),
                    new Claim(JwtRegisteredClaimNames.Sub,taikhoan.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim("ID",taikhoan.MaTk),
                    new Claim(ClaimTypes.Role,taikhoan.LoaiTaiKhoan),

                    //roles

                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    secretKeyBytes),SecurityAlgorithms.HmacSha512Signature)
                
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            var accessToken =  jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            //Lưu database
            var refreshTokenEntity = new RefreshToken
            {
                TokenID = Guid.NewGuid(),
                JwtID = token.Id,
                MaTk = taikhoan.MaTk,
                Token = refreshToken,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddHours(1)
            };

            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }    
        }

        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false // ko kierem tra token het han
            };
            try
            {
                //Check 1:Access Token valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.AccessToken,tokenValidateParam,
                    out var validatedToken);

                //check 2: check thuat toan
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,StringComparison
                        .InvariantCultureIgnoreCase);

                    if(!result)
                    {
                        return Ok( new ApiResponse
                        {
                            Success = false,
                            Message = "Invalid Token"
                        });
                    }
                    //check 3 :kiem tra token hết hạn chưa

                    var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(
                        x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                    var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                    if(expireDate>DateTime.UtcNow)
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Access token has not yet expired"
                        });
                    }
                    
                    //check 4 :Check refreshtoken exist in DB
                    var storedToken = _context.RefreshTokens.FirstOrDefault(x=>x.Token == model.RefreshToken);
                    if(storedToken is null)
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Refresh token không tồn tại"
                        });
                    } 
                    //check 5: refresh is used/revoked?
                    if(storedToken.IsUsed)
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Refresh token đã được sử dụng"
                        });
                    }
                    if (storedToken.IsRevoked)
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Refresh token đã được thu hồi"
                        });
                    }
                    //check 6: AccessToken id  == jwtId in RefreshToken
                    var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                    if(storedToken.JwtID!=jti)
                    {
                        return Ok(new ApiResponse
                        {
                            Success = false,
                            Message = "Token không được match"
                        });
                    }

                    //Update token is used
                    storedToken.IsRevoked = true;
                    storedToken.IsUsed = true;
                    _context.Update(storedToken);
                    await _context.SaveChangesAsync();

                    //Create new token

                    var user = await _context.Taikhoans.SingleOrDefaultAsync(x => x.MaTk == storedToken.MaTk);
                    var token = await GenerateToken(user);

                    return Ok(new ApiResponse
                    {
                        Success = true,
                        Message = "Renew token thành công",
                        Data = token
                    });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Lỗi"
                });
            }
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = "Lỗi"
            });
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    }
}

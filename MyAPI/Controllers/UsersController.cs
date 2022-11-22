using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyAPI.Data;
using MyAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public IActionResult Validate(LoginModel model)
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
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Đăng nhập thành công",
                Data = GenerateToken(user)
            });
        }

        private string GenerateToken(Taikhoan taikhoan)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Tên tài khoản:",taikhoan.TenTaiKhoan),
                    new Claim(ClaimTypes.Email,taikhoan.Email),
                    new Claim("ID",taikhoan.MaTk),

                    //roles

                    new Claim("TokenID",Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    secretKeyBytes),SecurityAlgorithms.HmacSha512Signature)
                
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}

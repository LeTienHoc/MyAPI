using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly AppSetting _appSettings;
        private readonly IKhachhangRepository _khrepo;

        public CustomersController(MyDbContext context, IOptionsMonitor<AppSetting> optionsMonitor, IKhachhangRepository khrepo)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
            _khrepo = khrepo;
        }
        [HttpPost("LoginKhachhang")]
        public IActionResult Validate(LoginModel model)
        {
            var customer = _context.Khachhangs.SingleOrDefault(
                p => p.TenTaiKhoan == model.TenTaiKhoan && p.MatKhau == model.MatKhau);

            if (customer == null)
            {
                return BadRequest(new ApiResponse
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
                Data = GenerateToken(customer)
            });
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(KhachhangModel model)
        {
            try
            {
                var newKhachhangId = await _khrepo.Add(model);
                if (newKhachhangId == null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Tuổi phải lớn hơn 16"
                    });
                }
                else
                {
                    if (newKhachhangId == "")
                    {
                        return BadRequest(new ApiResponse
                        {
                            Success = false,
                            Message = "Mật khẩu không khớp"
                        });
                    }

                    var Khachhang = await _khrepo.GetByID(newKhachhangId);
                    return Khachhang == null ? NotFound() : Ok(Khachhang);
                }

            }
            catch
            {
                return BadRequest();
            }
        }

        private string GenerateToken(Khachhang khachhang)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name,khachhang.TenKh!),
                    new Claim("TaiKhoanKH:",khachhang.TenTaiKhoan!),
                    new Claim(ClaimTypes.Email,khachhang.Email!),
                    new Claim("ID",khachhang.MaKh),

                    //roles

                    new Claim("TokenID",Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                    secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);

            return jwtTokenHandler.WriteToken(token);
        }
    }
}

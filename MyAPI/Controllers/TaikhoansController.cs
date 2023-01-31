using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;
using System.Security.Cryptography;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaikhoansController : ControllerBase
    {
        private readonly ITaikhoanRepository _TaikhoanRepo;
        private readonly MyDbContext _context;

        public TaikhoansController(ITaikhoanRepository repo,MyDbContext context)
        {
            _TaikhoanRepo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllTaikhoan()
        {
            try
            {
                return Ok(await _TaikhoanRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaikhoanByID(string id)
        {
            var Taikhoan= await _TaikhoanRepo.GetByID(id);
            return  Taikhoan==null ?NotFound():Ok(Taikhoan);
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> AddNewTaikhoan(TaikhoanModel model)
        {
            try
            {
                var sltrung = _context.Taikhoans.Where(x => x.TenTaiKhoan == model.TenTaiKhoan).Count();
                if(sltrung > 0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success=false,
                        Message="Tài khoản bị trùng"
                    });
                }    
                var newTaikhoanId = await _TaikhoanRepo.Add(model);
                    var Taikhoan = await _TaikhoanRepo.GetByID(newTaikhoanId);
                    return Taikhoan == null ? NotFound() : Ok(Taikhoan);               
            }
            catch
            {
                return BadRequest();
            }
            
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaikhoan(string id,TaikhoanModel model)
        {
            if(model.MatKhau==model.ConfirmMatkhau)
            {
                await _TaikhoanRepo.Update(id, model);
                return Ok(new ApiResponse
                {
                    Success=true,
                    Message="Update thành công"
                });
            }  
            else
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Mật khẩu không khớp"
                });
            }    
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaikhoan([FromRoute] string id)
        {
            await _TaikhoanRepo.Delete(id);
            return Ok();
        }
    }
}
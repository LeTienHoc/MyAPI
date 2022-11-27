using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    //[Authorize(Roles ="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class KhachhangsController : ControllerBase
    {
        private readonly IKhachhangRepository _KhachhangRepo;

        public KhachhangsController(IKhachhangRepository repo)
        {
            _KhachhangRepo = repo;
        }
        [HttpGet]
        public async Task< IActionResult> GetAllKhachhang()
        {
            try
            {
                return Ok(await _KhachhangRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKhachhangByID(string id)
        {
            var Khachhang= await _KhachhangRepo.GetByID(id);
            return  Khachhang==null ?NotFound():Ok(Khachhang);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewKhachhang(KhachhangModel model)
        {
            try
            {
                var newKhachhangId = await _KhachhangRepo.Add(model);
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

                    var Khachhang = await _KhachhangRepo.GetByID(newKhachhangId);
                    return Khachhang == null ? NotFound() : Ok(Khachhang);
                }

            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKhachhang(string id,KhachhangModel model)
        {
           var updatekh = await _KhachhangRepo.Update(id, model);
            if(updatekh == null)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Tuổi phải lớn hơn 16"
                });
            } 
            else
            {
                if(updatekh == "")
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Mật khẩu không khớp"
                    });
                }    
            }    
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhachhang([FromRoute] string id)
        {
            await _KhachhangRepo.Delete(id);
            return Ok();
        }
    }
}
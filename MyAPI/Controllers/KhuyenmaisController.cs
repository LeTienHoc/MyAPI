using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class KhuyenmaisController : ControllerBase
    {
        private readonly IKhuyenmaiRepository _KhuyenmaiRepo;

        public KhuyenmaisController(IKhuyenmaiRepository repo)
        {
            _KhuyenmaiRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllKhuyenmai()
        {
            try
            {
                return Ok(await _KhuyenmaiRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKhuyenmaiByID(string id)
        {
            var Khuyenmai= await _KhuyenmaiRepo.GetByID(id);
            return  Khuyenmai==null ?NotFound():Ok(Khuyenmai);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewKhuyenmai(KhuyenmaiModel model)
        {
            try
            {

                var newKhuyenmaiId = await _KhuyenmaiRepo.Add(model);
                if(newKhuyenmaiId!=null)
                {
                    var Khuyenmai = await _KhuyenmaiRepo.GetByID(newKhuyenmaiId);
                    return Khuyenmai == null ? NotFound() : Ok(Khuyenmai);
                }    
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Ngày bắt đầu phải trước ngày kết thúc"
                    });
                }    
                
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKhuyenmai(string id,KhuyenmaiModel model)
        {
            await _KhuyenmaiRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhuyenmai([FromRoute] string id)
        {
            await _KhuyenmaiRepo.Delete(id);
            return Ok();
        }
    }
}
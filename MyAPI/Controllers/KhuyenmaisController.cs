using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
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
                var Khuyenmai = await _KhuyenmaiRepo.GetByID(newKhuyenmaiId);
                return Khuyenmai == null ? NotFound() : Ok(Khuyenmai);
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichchieuKhuyenmaisController : ControllerBase
    {
        private readonly ILichchieuKhuyenmaiRepository _LichchieuKhuyenmaiRepo;

        public LichchieuKhuyenmaisController(ILichchieuKhuyenmaiRepository repo)
        {
            _LichchieuKhuyenmaiRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllLichchieuKhuyenmai()
        {
            try
            {
                return Ok(await _LichchieuKhuyenmaiRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLichchieuKhuyenmaiByID(string id)
        {
            var LichchieuKhuyenmai= await _LichchieuKhuyenmaiRepo.GetByID(id);
            return  LichchieuKhuyenmai==null ?NotFound():Ok(LichchieuKhuyenmai);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewLichchieuKhuyenmai(LichchieuKhuyenmaiModel model)
        {
            try
            {
                var newLichchieuKhuyenmaiId = await _LichchieuKhuyenmaiRepo.Add(model);
                var LichchieuKhuyenmai = await _LichchieuKhuyenmaiRepo.GetByID(newLichchieuKhuyenmaiId);
                return LichchieuKhuyenmai == null ? NotFound() : Ok(LichchieuKhuyenmai);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLichchieuKhuyenmai(string id,LichchieuKhuyenmaiModel model)
        {
            await _LichchieuKhuyenmaiRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichchieuKhuyenmai([FromRoute] string id)
        {
            await _LichchieuKhuyenmaiRepo.Delete(id);
            return Ok();
        }
    }
}
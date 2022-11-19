using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinhluansController : ControllerBase
    {
        private readonly IBinhluanRepository _BinhluanRepo;

        public BinhluansController(IBinhluanRepository repo)
        {
            _BinhluanRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllBinhluan()
        {
            try
            {
                return Ok(await _BinhluanRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBinhluanByID(string id)
        {
            var Binhluan= await _BinhluanRepo.GetByID(id);
            return  Binhluan==null ?NotFound():Ok(Binhluan);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBinhluan(BinhluanModel model)
        {
            try
            {
                var newBinhluanId = await _BinhluanRepo.Add(model);
                var Binhluan = await _BinhluanRepo.GetByID(newBinhluanId);
                return Binhluan == null ? NotFound() : Ok(Binhluan);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBinhluan(string id,BinhluanModel model)
        {
            await _BinhluanRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBinhluan([FromRoute] string id)
        {
            await _BinhluanRepo.Delete(id);
            return Ok();
        }
    }
}
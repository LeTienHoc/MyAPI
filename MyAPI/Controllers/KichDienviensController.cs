using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KichDienviensController : ControllerBase
    {
        private readonly IKichDienvienRepository _KichDienvienRepo;

        public KichDienviensController(IKichDienvienRepository repo)
        {
            _KichDienvienRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllKichDienvien()
        {
            try
            {
                return Ok(await _KichDienvienRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKichDienvienByID(string id)
        {
            var KichDienvien= await _KichDienvienRepo.GetByID(id);
            return  KichDienvien==null ?NotFound():Ok(KichDienvien);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewKichDienvien(KichDienvienModel model)
        {
            try
            {
                var newKichDienvienId = await _KichDienvienRepo.Add(model);
                var KichDienvien = await _KichDienvienRepo.GetByID(newKichDienvienId);
                return KichDienvien == null ? NotFound() : Ok(KichDienvien);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKichDienvien(string id,KichDienvienModel model)
        {
            await _KichDienvienRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKichDienvien([FromRoute] string id)
        {
            await _KichDienvienRepo.Delete(id);
            return Ok();
        }
    }
}
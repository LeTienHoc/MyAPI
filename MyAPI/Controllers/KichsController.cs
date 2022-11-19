using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KichsController : ControllerBase
    {
        private readonly IKichRepository _KichRepo;

        public KichsController(IKichRepository repo)
        {
            _KichRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllKich()
        {
            try
            {
                return Ok(await _KichRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKichByID(string id)
        {
            var Kich= await _KichRepo.GetByID(id);
            return  Kich==null ?NotFound():Ok(Kich);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewKich(KichModel model)
        {
            try
            {
                var newKichId = await _KichRepo.Add(model);
                var Kich = await _KichRepo.GetByID(newKichId);
                return Kich == null ? NotFound() : Ok(Kich);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKich(string id,KichModel model)
        {
            await _KichRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKich([FromRoute] string id)
        {
            await _KichRepo.Delete(id);
            return Ok();
        }
    }
}
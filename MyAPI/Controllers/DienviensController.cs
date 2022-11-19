using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DienviensController : ControllerBase
    {
        private readonly IDienvienRepository _DienvienRepo;

        public DienviensController(IDienvienRepository repo)
        {
            _DienvienRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllDienvien()
        {
            try
            {
                return Ok(await _DienvienRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDienvienByID(string id)
        {
            var Dienvien= await _DienvienRepo.GetByID(id);
            return  Dienvien==null ?NotFound():Ok(Dienvien);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDienvien(DienvienModel model)
        {
            try
            {
                var newDienvienId = await _DienvienRepo.Add(model);
                var Dienvien = await _DienvienRepo.GetByID(newDienvienId);
                return Dienvien == null ? NotFound() : Ok(Dienvien);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDienvien(string id,DienvienModel model)
        {
            await _DienvienRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDienvien([FromRoute] string id)
        {
            await _DienvienRepo.Delete(id);
            return Ok();
        }
    }
}
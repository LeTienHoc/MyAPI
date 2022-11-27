using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaodiensController : ControllerBase
    {
        private readonly IDaodienRepository _daodienRepo;

        public DaodiensController(IDaodienRepository repo)
        {
            _daodienRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllDaodien()
        {
            try
            {
                return Ok(await _daodienRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDaodienByID(string id)
        {
            var daodien= await _daodienRepo.GetByID(id);
            return  daodien==null ?NotFound():Ok(daodien);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDaodien(DaodienModel model)
        {
            try
            {
                var newDaodienId = await _daodienRepo.Add(model);
                var daodien = await _daodienRepo.GetByID(newDaodienId);
                return daodien == null ? NotFound() : Ok(daodien);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDaodien(string id,DaodienModel model)
        {
            await _daodienRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDaodien([FromRoute] string id)
        {
            await _daodienRepo.Delete(id);
            return Ok();
        }
    }
}
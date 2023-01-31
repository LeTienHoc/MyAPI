using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NhakichsController : ControllerBase
    {
        private readonly INhakichRepository _NhakichRepo;
        private readonly MyDbContext _context;

        public NhakichsController(INhakichRepository repo,MyDbContext context)
        {
            _NhakichRepo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllNhakich()
        {
            try
            {
                return Ok(await _NhakichRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNhakichByID(string id)
        {
            var Nhakich= await _NhakichRepo.GetByID(id);
            return  Nhakich==null ?NotFound():Ok(Nhakich);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewNhakich(NhakichModel model)
        {
            try
            {
                
                var newNhakichId = await _NhakichRepo.Add(model);
                var Nhakich = await _NhakichRepo.GetByID(newNhakichId);
                return Nhakich == null ? NotFound() : Ok(Nhakich);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNhakich(string id,NhakichModel model)
        {
            await _NhakichRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNhakich([FromRoute] string id)
        {
            await _NhakichRepo.Delete(id);
            return Ok();
        }
    }
}
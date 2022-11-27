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
    public class GhesController : ControllerBase
    {
        private readonly IGheRepository _GheRepo;

        public GhesController(IGheRepository repo)
        {
            _GheRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllGhe()
        {
            try
            {
                return Ok(await _GheRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGheByID(string id)
        {
            var Ghe= await _GheRepo.GetByID(id);
            return  Ghe==null ?NotFound():Ok(Ghe);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddNewGhe(GheModel model)
        {
            try
            {
                var newGheId = await _GheRepo.Add(model);
                var Ghe = await _GheRepo.GetByID(newGheId);
                return Ghe == null ? NotFound() : Ok(Ghe);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGhe(string id,GheModel model)
        {
            await _GheRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGhe([FromRoute] string id)
        {
            await _GheRepo.Delete(id);
            return Ok();
        }
    }
}
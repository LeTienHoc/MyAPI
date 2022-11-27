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
    public class VesController : ControllerBase
    {
        private readonly IVeRepository _VeRepo;

        public VesController(IVeRepository repo)
        {
            _VeRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllVe()
        {
            try
            {
                return Ok(await _VeRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVeByID(string id)
        {
            var Ve= await _VeRepo.GetByID(id);
            return  Ve==null ?NotFound():Ok(Ve);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewVe(VeModel model)
        {
            try
            {
                var newVeId = await _VeRepo.Add(model);
                var Ve = await _VeRepo.GetByID(newVeId);
                return Ve == null ? NotFound() : Ok(Ve);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVe(string id,VeModel model)
        {
            await _VeRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVe([FromRoute] string id)
        {
            await _VeRepo.Delete(id);
            return Ok();
        }
    }
}
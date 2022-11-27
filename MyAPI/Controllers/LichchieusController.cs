using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class LichchieusController : ControllerBase
    {
        private readonly ILichchieuRepository _LichchieuRepo;

        public LichchieusController(ILichchieuRepository repo)
        {
            _LichchieuRepo = repo;
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Sta)]
        public async Task< IActionResult> GetAllLichchieu()
        {
            try
            {
                return Ok(await _LichchieuRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLichchieuByID(string id)
        {
            var Lichchieu= await _LichchieuRepo.GetByID(id);
            return  Lichchieu==null ?NotFound():Ok(Lichchieu);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewLichchieu(LichchieuModel model)
        {
            try
            {
                var newLichchieuId = await _LichchieuRepo.Add(model);
                var Lichchieu = await _LichchieuRepo.GetByID(newLichchieuId);
                return Lichchieu == null ? NotFound() : Ok(Lichchieu);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLichchieu(string id,LichchieuModel model)
        {
            await _LichchieuRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichchieu([FromRoute] string id)
        {
            await _LichchieuRepo.Delete(id);
            return Ok();
        }
    }
}
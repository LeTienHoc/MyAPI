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
    public class XuatchieusController : ControllerBase
    {
        private readonly IXuatchieuRepository _XuatchieuRepo;

        public XuatchieusController(IXuatchieuRepository repo)
        {
            _XuatchieuRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllXuatchieu()
        {
            try
            {
                return Ok(await _XuatchieuRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetXuatchieuByID(string id)
        {
            var Xuatchieu= await _XuatchieuRepo.GetByID(id);
            return  Xuatchieu==null ?NotFound():Ok(Xuatchieu);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewXuatchieu(XuatchieuModel model)
        {
            try
            {
                var newXuatchieuId = await _XuatchieuRepo.Add(model);
                var Xuatchieu = await _XuatchieuRepo.GetByID(newXuatchieuId);
                return Xuatchieu == null ? NotFound() : Ok(Xuatchieu);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateXuatchieu(string id,XuatchieuModel model)
        {
            await _XuatchieuRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteXuatchieu([FromRoute] string id)
        {
            await _XuatchieuRepo.Delete(id);
            return Ok();
        }
    }
}
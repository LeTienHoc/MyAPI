using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaikhoansController : ControllerBase
    {
        private readonly ITaikhoanRepository _TaikhoanRepo;

        public TaikhoansController(ITaikhoanRepository repo)
        {
            _TaikhoanRepo = repo;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllTaikhoan()
        {
            try
            {
                return Ok(await _TaikhoanRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaikhoanByID(string id)
        {
            var Taikhoan= await _TaikhoanRepo.GetByID(id);
            return  Taikhoan==null ?NotFound():Ok(Taikhoan);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewTaikhoan(TaikhoanModel model)
        {
            try
            {
                var newTaikhoanId = await _TaikhoanRepo.Add(model);
                var Taikhoan = await _TaikhoanRepo.GetByID(newTaikhoanId);
                return Taikhoan == null ? NotFound() : Ok(Taikhoan);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaikhoan(string id,TaikhoanModel model)
        {
            await _TaikhoanRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaikhoan([FromRoute] string id)
        {
            await _TaikhoanRepo.Delete(id);
            return Ok();
        }
    }
}
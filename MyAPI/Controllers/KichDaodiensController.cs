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
    public class KichDaodiensController : ControllerBase
    {
        private readonly IKichDaodienRepository _KichDaodienRepo;
        private readonly MyDbContext _context;

        public KichDaodiensController(IKichDaodienRepository repo,MyDbContext context)
        {
            _KichDaodienRepo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllKichDaodien()
        {
            try
            {
                return Ok(await _KichDaodienRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKichDaodienByID(string id)
        {
            var KichDaodien= await _KichDaodienRepo.GetByID(id);
            return  KichDaodien==null ?NotFound():Ok(KichDaodien);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewKichDaodien(KichDaodienModel model)
        {
            try
            {
                
                var newKichDaodienId = await _KichDaodienRepo.Add(model);
                if(newKichDaodienId==null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success=false,
                        Message="Nhà kịch không có đạo diễn này"
                    });
                }    
                var KichDaodien = await _KichDaodienRepo.GetByID(newKichDaodienId);
                return KichDaodien == null ? NotFound() : Ok(KichDaodien);
            }
            catch
            {
                return BadRequest();
            }
            
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKichDaodien(string id,KichDaodienModel model)
        {
            await _KichDaodienRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKichDaodien([FromRoute] string id)
        {
            await _KichDaodienRepo.Delete(id);
            return Ok();
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;
using System.Security.Claims;

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
        [Authorize]
        [HttpGet]
        [Route("KichDaoDienCuaMoiNhakich")]
        public async Task<IActionResult> GetAllKichDaoDienNK()
        {
            
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();
                var daodien = (from kd in _context.KichDaodiens
                                join d in _context.Daodiens on kd.MaDaodien equals d.MaDaoDien
                                where d.MaNhaKich!.Equals("" + mank + "")
                                select new
                                {
                                    MaDaoDien=d.MaDaoDien,
                                    MaKich=kd.MaKich,
                                    TenDaoDien=d.TenDaoDien
                                }).ToList();
                return Ok(daodien);
            }
            catch
            {
                return BadRequest();
            }
        }
        //[HttpGet]
        //public async Task< IActionResult> GetAllKichDaodien()
        //{
        //    try
        //    {
        //        return Ok(await _KichDaodienRepo.GetAll());
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

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
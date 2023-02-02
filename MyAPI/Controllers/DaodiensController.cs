using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Security.Claims;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class DaodiensController : ControllerBase
    {
        private readonly IDaodienRepository _daodienRepo;
        private readonly MyDbContext _context;

        public DaodiensController(IDaodienRepository repo,MyDbContext context)
        {
            _daodienRepo = repo;
            _context = context;
        }
        //[Authorize]
        //[HttpGet]
        //public async Task< IActionResult> GetAllDaodien()
        //{
        //    try
        //    {
        //        
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}
        [Authorize]
        [HttpGet]
        [Route("DaoDienCuaMoiNhakich")]
        public async Task<IActionResult> GetAllDaodienNK()
        {
            try
            {
                string role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value!;
                if(role.Equals("admin"))
                {
                    return Ok(await _daodienRepo.GetAll());
                }  
                else if(role.Equals("Quản lý"))
                {
                    string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                    var mank = (from nk in _context.Nhakiches
                                where nk.MaNhaKich == idtaikhoan
                                select nk.MaNhaKich).SingleOrDefault()!.ToString();
                    var daodien = (from dd in _context.Daodiens
                                   where dd.MaNhaKich!.Equals("" + mank + "")
                                   select new
                                   {
                                       MaDaoDien = dd.MaDaoDien,
                                       MaNhaKich = dd.MaNhaKich,
                                       TenDaoDien = dd.TenDaoDien
                                   }).ToList();
                    return Ok(daodien);
                }
                return Ok();
                
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewDaodien(DaodienModel model)
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();


                model.MaNhaKich = mank;
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
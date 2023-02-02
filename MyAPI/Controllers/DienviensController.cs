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
    public class DienviensController : ControllerBase
    {
        private readonly IDienvienRepository _DienvienRepo;
        private readonly MyDbContext _context;

        public DienviensController(IDienvienRepository repo,MyDbContext context)
        {
            _DienvienRepo = repo;
            _context = context;
        }
        [Authorize]
        [HttpGet]
        [Route("DienVienCuaMoiNhakich")]
        public async Task<IActionResult> GetAllDienvienNK()
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();
                var dienvien = (from dv in _context.Dienviens
                               where dv.MaNhaKich!.Equals("" + mank + "")
                               select new
                               {
                                   MaDienVien = dv.MaDienVien,
                                   MaNhaKich = dv.MaNhaKich,
                                   TenDienVien = dv.TenDienVien
                               }).ToList();
                return Ok(dienvien);
            }
            catch
            {
                return BadRequest();
            }
        }
        //[HttpGet]
        //public async Task< IActionResult> GetAllDienvien()
        //{
        //    try
        //    {
        //        return Ok(await _DienvienRepo.GetAll());
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDienvienByID(string id)
        {
            var Dienvien= await _DienvienRepo.GetByID(id);
            return  Dienvien==null ?NotFound():Ok(Dienvien);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNewDienvien(DienvienModel model)
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();

               
                model.MaNhaKich = mank;
                var newDienvienId = await _DienvienRepo.Add(model);
                var Dienvien = await _DienvienRepo.GetByID(newDienvienId);
                return Dienvien == null ? NotFound() : Ok(Dienvien);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDienvien(string id,DienvienModel model)
        {
            await _DienvienRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDienvien([FromRoute] string id)
        {
            await _DienvienRepo.Delete(id);
            return Ok();
        }
    }
}
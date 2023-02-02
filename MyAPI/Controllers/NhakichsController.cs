using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;
using System.Security.Claims;

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
        [Authorize]
        [HttpGet]
        [Route("NhaKich")]
        public async Task<IActionResult> GetAllNK()
        {

            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();
                var nhakich = (from kd in _context.Nhakiches
                                 where kd.MaNhaKich!.Equals("" + mank + "")
                                 select new
                                 {
                                     MaNhaKich=kd.MaNhaKich,
                                     TenNhaKich=kd.TenNhaKich,
                                     SoDienThoai=kd.SoDienThoai,
                                     DiaChi=kd.DiaChi,
                                     Email=kd.Email
                                 }).ToList();
                return Ok(nhakich);
            }
            catch
            {
                return BadRequest();
            }
        }
        //[HttpGet]
        //public async Task< IActionResult> GetAllNhakich()
        //{
        //    try
        //    {
        //        return Ok(await _NhakichRepo.GetAll());
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

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
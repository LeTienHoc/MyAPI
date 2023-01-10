using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;
using System.Drawing.Printing;
using System.Security.Claims;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class KichsController : ControllerBase
    {
        private readonly IKichRepository _KichRepo;
        private readonly MyDbContext _context;

        public KichsController(IKichRepository repo,MyDbContext context)
        {
            _KichRepo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllKich()
        {
            try
            {
                return Ok(await _KichRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKichByID(string id)
        {
            var Kich= await _KichRepo.GetByID(id);
            return  Kich==null ?NotFound():Ok(Kich);
        }
        
        [Authorize(Roles = "quantrinhakich")]
        [HttpPost]
        public async Task<IActionResult> AddNewKich(KichModel model)
        {
            try
            {
                
                
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.TenNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault().ToString();

               // var mank1 = _context.Nhakiches.Select(x => x.TenNhaKich == idtaikhoan);
                model.MaNhaKich = mank;  
                var newKichId = await _KichRepo.Add(model);
                var Kich = await _KichRepo.GetByID(newKichId);
                return Kich == null ? NotFound() : Ok(Kich);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKich(string id,KichModel model)
        {
            await _KichRepo.Update(id, model);
            return Ok();
        }
        [Route("Duyet Kich")]
        [HttpPut]
        public async Task<IActionResult> DuyetKich(string id,UpdateModel model)
        {
            await _KichRepo.DuyetKich(id,model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKich([FromRoute] string id)
        {
            await _KichRepo.Delete(id);
            return Ok();
        }
        [Route("Search")]
        [HttpGet]
        public IActionResult GetallKichs(string search,int page=1)
        { 
            try
            {
                var result = _KichRepo.Getallkichs(search);
                if (result == null)
                {
                    return BadRequest("không tìm thấy");
                }
                return Ok(result);
            }
            catch
            {
                return BadRequest("Lỗi");
            }
        }
        [Route("Detail")]
        [HttpGet]
        public IActionResult Detail(string id)
        {
            try
            {
                var result = _KichRepo.Detail(id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
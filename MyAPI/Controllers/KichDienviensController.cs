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
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class KichDienviensController : ControllerBase
    {
        private readonly IKichDienvienRepository _KichDienvienRepo;
        private readonly MyDbContext _context;

        public KichDienviensController(IKichDienvienRepository repo,MyDbContext context)
        {
            _KichDienvienRepo = repo;
            _context = context;
        }
        [Authorize]
        [HttpGet]
        [Route("KichDienVienCuaMoiNhakich")]
        public async Task<IActionResult> GetAllKichDienVienNK()
        {

            try
            {
                string role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value!;
                if(role.Equals("admin"))
                {
                    return Ok(await _KichDienvienRepo.GetAll());
                }    
                else if(role.Equals("Quản lý"))
                {
                    string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                    var mank = (from nk in _context.Nhakiches
                                where nk.MaNhaKich == idtaikhoan
                                select nk.MaNhaKich).SingleOrDefault()!.ToString();
                    var dienvien = (from kd in _context.KichDienviens
                                    join d in _context.Dienviens on kd.MaDienVien equals d.MaDienVien
                                    where d.MaNhaKich!.Equals("" + mank + "")
                                    select new
                                    {
                                        MaDienVien = d.MaDienVien,
                                        MaKich = kd.MaKich,
                                        TenDienVien = d.TenDienVien
                                    }).ToList();
                    return Ok(dienvien);
                }
                return Ok();
                
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task< IActionResult> GetAllKichDienvien()
        {
            try
            {
                return Ok(await _KichDienvienRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKichDienvienByID(string id)
        {
            var KichDienvien= await _KichDienvienRepo.GetByID(id);
            return  KichDienvien==null ?NotFound():Ok(KichDienvien);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewKichDienvien(KichDienvienModel model)
        {
            try
            {
                
                var newKichDienvienId = await _KichDienvienRepo.Add(model);
                if(newKichDienvienId==null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success=false,
                        Message="Nhà kịch không có diễn viên này"
                    });
                }    
                var KichDienvien = await _KichDienvienRepo.GetByID(newKichDienvienId);
                return KichDienvien == null ? NotFound() : Ok(KichDienvien);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKichDienvien(string id,KichDienvienModel model)
        {
            await _KichDienvienRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKichDienvien([FromRoute] string id)
        {
            await _KichDienvienRepo.Delete(id);
            return Ok();
        }
    }
}
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
    public class LichchieusController : ControllerBase
    {
        private readonly ILichchieuRepository _LichchieuRepo;
        private readonly MyDbContext _context;

        public LichchieusController(ILichchieuRepository repo,MyDbContext context)
        {
            _LichchieuRepo = repo;
            _context = context;
        }
        [Authorize]
        [HttpGet]
        [Route("LichChieuCuaMoiNhakich")]
        public async Task<IActionResult> GetAllLichChieuNK()
        {

            try
            {
                string role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value!;
                if(role.Equals("admin"))
                {
                    return Ok(await _LichchieuRepo.GetAll());
                }   
                else if(role.Equals("Quản lý"))
                {
                    string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                    var mank = (from nk in _context.Nhakiches
                                where nk.MaNhaKich == idtaikhoan
                                select nk.MaNhaKich).SingleOrDefault()!.ToString();
                    var lichchieu = (from kd in _context.Lichchieus
                                     where kd.MaNhaKich!.Equals("" + mank + "")
                                     select new
                                     {
                                         MaLichChieu = kd.MaLichChieu,
                                         NgayBd = kd.NgayBd,
                                         NgayKt = kd.NgayKt,
                                         MaNhaKich = kd.MaNhaKich
                                     }).ToList();
                    return Ok(lichchieu);
                }    
                
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
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
        [Authorize(Roles = "quantrinhakich")]
        [HttpPost]
        public async Task<IActionResult> AddNewLichchieu(LichchieuModel model)
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();

                model.MaNhaKich = mank;
                if(model.NgayBd<model.NgayKt)
                {
                    var ktralc = (from lc in _context.Lichchieus
                                  where lc.NgayBd <= model.NgayBd && lc.NgayKt >= model.NgayBd && lc.MaNhaKich == "" + mank + ""
                                  select lc.MaLichChieu).ToList().Count();
                    if (ktralc == 0)
                    {

                        var newLichchieuId = await _LichchieuRepo.Add(model);
                        var Lichchieu = await _LichchieuRepo.GetByID(newLichchieuId);
                        return Lichchieu == null ? NotFound() : Ok(Lichchieu);

                    }
                    else
                    {
                        return BadRequest(new ApiResponse
                        {
                            Success = false,
                            Message = "Lịch trùng , đã có lịch chiếu"
                        });
                    }
                } 
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success=false,
                        Message="Ngày Bắt đầu phải trước ngày kết thúc"
                    });
                }    
            }
            catch
            {
                return BadRequest();
            }
            
        
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLichchieu(string id,LichchieuModel model)
        {
            string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var mank = (from nk in _context.Nhakiches
                        where nk.MaNhaKich == idtaikhoan
                        select nk.MaNhaKich).SingleOrDefault()!.ToString();
            if (model.NgayBd < model.NgayKt)
            {
                var ktralc = (from lc in _context.Lichchieus
                              where lc.NgayBd <= model.NgayBd && lc.NgayKt >= model.NgayBd && lc.MaNhaKich == "" + mank + ""
                              select lc.MaLichChieu).ToList().Count();
                if (ktralc == 0)
                {
                    await _LichchieuRepo.Update(id, model);
                    return Ok();
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Lịch trùng , đã có lịch chiếu"
                    });
                }
            }
            else
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Ngày Bắt đầu phải trước ngày kết thúc"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichchieu([FromRoute] string id)
        {
            await _LichchieuRepo.Delete(id);
            return Ok();
        }
    }
}
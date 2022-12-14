using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System;
using System.Data;
using System.Globalization;
using System.Security.Claims;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class XuatchieusController : ControllerBase
    {
        private readonly IXuatchieuRepository _XuatchieuRepo;
        private readonly MyDbContext _context;

        public XuatchieusController(IXuatchieuRepository repo,MyDbContext context)
        {
            _XuatchieuRepo = repo;
            _context = context;
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
        [Authorize(Roles = "quantrinhakich")]
        [HttpPost]
        public async Task<IActionResult> AddNewXuatchieu(XuatchieuModel model)
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.TenNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()?.ToString();
                
                var select = (from lc in _context.Lichchieus
                              where lc.NgayBd <= model.NgayGio && lc.NgayKt >= model.NgayGio && lc.MaNhaKich==""+mank+""
                              select lc.MaLichChieu).ToList()?.Count();
                
                if (select >= 1)
                {
                    var kich = (from k in _context.Kiches
                                where k.MaNhaKich == "" + mank + "" && k.TrangThai == 1 && k.MaKich == model.MaKich &&
                                k.NgayBd<=model.NgayGio && k.NgayKt>=model.NgayGio
                                select k.MaKich).ToList().Count();

                    if (kich>0)
                    {
                    var ktlich = (from lc in _context.Lichchieus
                                  where lc.MaNhaKich == "" + mank + "" && lc.NgayBd <= model.NgayGio && lc.NgayKt >= model.NgayGio
                                  select lc.MaLichChieu).SingleOrDefault()?.ToString();
                        if (model.MaLichChieu==ktlich)
                        {
                        
                            var newXuatchieuId = await _XuatchieuRepo.Add(model);
                            var Xuatchieu = await _XuatchieuRepo.GetByID(newXuatchieuId);
                            return Xuatchieu == null ? NotFound() : Ok(Xuatchieu);
                        }  
                        else
                        {
                            return BadRequest(new ApiResponse
                            {
                                Success = false,
                                Message = "Kịch không có lịch chiếu"
                            });
                        }    
                    }   
                    else
                    {
                        return BadRequest(new ApiResponse
                        {
                            Success=false,
                            Message="Không có kịch"
                        });
                    }
                }
                else
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Nhà kịch không có lịch chiếu kịch này"
                    });
                }
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
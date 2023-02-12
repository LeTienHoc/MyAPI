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
        [Authorize]
        [HttpGet]
        [Route("XuatChieuCuaMoiNhakich")]
        public async Task<IActionResult> GetAllXuatChieuNK()
        {

            try
            {
                string role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value!;
                if(role.Equals("admin"))
                {
                    return Ok(await _XuatchieuRepo.GetAll());
                }   
                else if(role.Equals("Quản lý"))
                {
                    string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                    var mank = (from nk in _context.Nhakiches
                                where nk.MaNhaKich == idtaikhoan
                                select nk.MaNhaKich).SingleOrDefault()!.ToString();
                    var xuatchieu = (from kd in _context.Xuatchieus
                                     join d in _context.Lichchieus on kd.MaLichChieu equals d.MaLichChieu                                   
                                     where d.MaNhaKich!.Equals("" + mank + "")
                                     select new
                                     {
                                         MaXc = kd.MaXc,
                                         MaKich = kd.MaKich,
                                         MaLichChieu = kd.MaLichChieu,
                                         NgayChieu = kd.NgayChieu,
                                         ThoiGian = kd.Thoigian
                                     }).ToList();
                    return Ok(xuatchieu);
                }
                return Ok();

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task< IActionResult> GetAllXuatchieu()
        {
            var xuatchieu = (from kd in _context.Xuatchieus
                             join d in _context.Lichchieus on kd.MaLichChieu equals d.MaLichChieu
                             join k in _context.Kiches on kd.MaKich equals k.MaKich
                             select new
                             {
                                 MaXc = kd.MaXc,
                                 MaKich = kd.MaKich,
                                 MaLichChieu = kd.MaLichChieu,
                                 NgayChieu = kd.NgayChieu,
                                 ThoiGian = kd.Thoigian,
                                 TenKich=k.TenKich,
                                 MaNhaKich=d.MaNhaKich
                             }).ToList();
            return Ok(xuatchieu);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetXuatchieuByID(string id)
        {
            var Xuatchieu= await _XuatchieuRepo.GetByID(id);
            return  Xuatchieu==null ?NotFound():Ok(Xuatchieu);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewXuatchieu(XuatchieuModel model)
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()?.ToString();
                
                var select = (from lc in _context.Lichchieus
                              where lc.NgayBd <= model.NgayChieu && lc.NgayKt >= model.NgayChieu && lc.MaNhaKich==""+mank+""
                              select lc.MaLichChieu).ToList()?.Count();
                

                if (select >= 1)
                {
                    var kich = (from k in _context.Kiches
                                where k.MaNhaKich == "" + mank + "" && k.TrangThai == 1 && k.MaKich == model.MaKich &&
                                k.NgayBd<=model.NgayChieu && k.NgayKt>=model.NgayChieu
                                select k.MaKich).ToList().Count();

                    if (kich>0)
                    {
                     var ktlich = (from lc in _context.Lichchieus
                                  where lc.MaNhaKich == "" + mank + "" && lc.NgayBd <= model.NgayChieu && lc.NgayKt >= model.NgayChieu
                                  select lc.MaLichChieu).SingleOrDefault()?.ToString();
                        if (model.MaLichChieu==ktlich)
                        {
                            var ngaychieu = (from k in _context.Kiches
                                             join xc in _context.Xuatchieus on k.MaKich equals xc.MaKich
                                             where k.MaNhaKich.Equals("" + mank + "") && xc.NgayChieu==model.NgayChieu
                                             select k.MaKich).Count();
                            if(ngaychieu>0)
                            {
                                return BadRequest(new ApiResponse
                                {
                                    Message = "Ngày hôm nay đã có xuất chiếu",
                                    Success = false
                                });
                            }    
                            else
                            {
                                var newXuatchieuId = await _XuatchieuRepo.Add(model);
                                var Xuatchieu = await _XuatchieuRepo.GetByID(newXuatchieuId);
                                return Xuatchieu == null ? NotFound() : Ok(Xuatchieu);
                            }    
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
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateXuatchieu(string id,XuatchieuModel model)
        {
            string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var mank = (from nk in _context.Nhakiches
                        where nk.MaNhaKich == idtaikhoan
                        select nk.MaNhaKich).SingleOrDefault()?.ToString();

            var select = (from lc in _context.Lichchieus
                          where lc.NgayBd <= model.NgayChieu && lc.NgayKt >= model.NgayChieu && lc.MaNhaKich == "" + mank + ""
                          select lc.MaLichChieu).ToList()?.Count();

            if (select >= 1)
            {
                var kich = (from k in _context.Kiches
                            where k.MaNhaKich == "" + mank + "" && k.TrangThai == 1 && k.MaKich == model.MaKich &&
                            k.NgayBd <= model.NgayChieu && k.NgayKt >= model.NgayChieu
                            select k.MaKich).ToList().Count();

                if (kich > 0)
                {
                    var ktlich = (from lc in _context.Lichchieus
                                  where lc.MaNhaKich == "" + mank + "" && lc.NgayBd <= model.NgayChieu && lc.NgayKt >= model.NgayChieu
                                  select lc.MaLichChieu).SingleOrDefault()?.ToString();
                    if (model.MaLichChieu == ktlich)
                    {

                        await _XuatchieuRepo.Update(id, model);
                        return Ok();
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
                        Success = false,
                        Message = "Không có kịch"
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteXuatchieu([FromRoute] string id)
        {
            await _XuatchieuRepo.Delete(id);
            return Ok();
            
        }
    }
}
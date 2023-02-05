using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Security.Claims;


namespace MyAPI.Controllers
{
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class VesController : ControllerBase
    {
        private readonly IVeRepository _VeRepo;
        private readonly MyDbContext _context;

        public VesController(IVeRepository repo,MyDbContext context)
        {
            _VeRepo = repo;
            _context = context;
        }

        [HttpGet]
        public async Task< IActionResult> GetAllVe()
        {
            try
            {
                return Ok(await _VeRepo.GetAll());
            }
            catch
            {
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Login/curent")]
        public async  Task<IActionResult> Getuser()
        {
            string id =  HttpContext.User.FindFirstValue("ID");
            return Ok(new {MaKh = id});
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVeByID(string id)
        {
            
            var Ve= await _VeRepo.GetByID(id);
            return  Ve==null ?NotFound():Ok(Ve);
        }
        
        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "quantrinhakich")]
        public async Task<IActionResult> AddNewVe(VeModel model)
        {
            
            string id = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value!;
            string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            //var count = (from ghes in _context.Ghes
            //             where ghes.MaNhaKich!.Equals("NK000000002") 
            //             select ghes.MaGhe).Count();
            

            if (id!=null || idtaikhoan!=null)
            {
                //var ktghe = from g in _context.Ghes
                //            join v in _context.Ves on g.MaGhe equals v.MaGhe
                //            where g.MaGhe.Equals(

                //                model.MaGhe) && g.MaNhaKich.Equals("");
                var makh = (from kh in _context.Khachhangs
                            where kh.TenKh!.Equals(id)
                            select kh.MaKh).SingleOrDefault()!.ToString();
                var manhakich = (from xc in _context.Xuatchieus
                                 join k in _context.Kiches on xc.MaKich equals k.MaKich
                                 where xc.MaXc.Equals("" + model.MaXc + "")
                                 select k.MaNhaKich).SingleOrDefault();
                var ghetrong = (from g in _context.Ghes
                                where g.MaNhaKich!.Equals("" + manhakich + "")
                                select g.MaGhe).ToList();
                var ghedamua = (from g in _context.Ghes
                               join v in _context.Ves on g.MaGhe equals v.MaGhe
                               join xc in _context.Xuatchieus on v.MaXc equals xc.MaXc
                               join k in _context.Kiches on xc.MaKich equals k.MaKich
                               where xc.MaXc.Equals("" + model.MaXc + "") && v.TinhTrang.Equals(1)
                               select g.MaGhe).ToList();
                var getghe = ghetrong.Except(ghedamua).ToList();
                var c = ghedamua.Where(t => t.Equals("" + model.MaGhe + "")).Count();
                if(c>0)
                {
                    return BadRequest(new ApiResponse
                    {
                        Message = "Vé đã có người đặt trước ròi",
                        Success=false,
                    });
                }    
                else
                {
                    model.MaKh = id;
                    model.MaTk = idtaikhoan;
                    model.TinhTrang = 1;
                    model.NgayDatVe = DateTime.Now;
                    var newVeId = await _VeRepo.Add(model);

                    var Ve = await _VeRepo.GetByID(newVeId);
                    return Ve == null ? NotFound() : Ok(Ve);
                }    
            } 
            else
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Bạn chưa đăng nhập"
                });
            }    
        }
        //public async Task<IActionResult> Thanhtoan()
        //{
        //    var ghedachon = (from gc in _context.Ves
        //                     where 
        //                     ).ToList();
        //}
        [Route("Ghe trong")]
        [HttpGet]
        public async Task<IActionResult> ShowGheConTrong(string xuatchieu)
        {
            var manhakich = (from xc in _context.Xuatchieus
                             join k in _context.Kiches on xc.MaKich equals k.MaKich
                             where xc.MaXc.Equals("" + xuatchieu + "")
                             select k.MaNhaKich).SingleOrDefault();
            var ghetrong = (from g in _context.Ghes
                            where g.MaNhaKich!.Equals("" + manhakich + "")
                            select new
                            {
                                MaGhe = g.MaGhe,
                                Hang=g.Hang,
                                Seat=g.Seat
                            }).ToList();
            var ghedamua = (from g in _context.Ghes
                           join v in _context.Ves on g.MaGhe equals v.MaGhe
                           join xc in _context.Xuatchieus on v.MaXc equals xc.MaXc
                           join k in _context.Kiches on xc.MaKich equals k.MaKich
                           where xc.MaXc.Equals(""+xuatchieu+"") && v.TinhTrang.Equals(1)
                            select new
                            {
                                MaGhe = g.MaGhe,
                                Hang = g.Hang,
                                Seat = g.Seat
                            }).ToList();
            var c = ghetrong.Except(ghedamua).ToList();
            return Ok(c);
        }

        //[HttpPost]
        //[Authorize]
        //[Route("Đặt Vé")]
        //public async Task<IActionResult> DatVe(string mave,string maxc,string makh,string matk,string maghe, int soluong)
        //{
        //    string id = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value!;
        //    string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        //    var count = (from ghes in _context.Ghes
        //                 where ghes.Status == 0 && ghes.NhaKich!.Equals("NK000000002")
        //                 select ghes.MaGhe).Count();

        //    if (soluong == 0)
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "Bạn chưa chọn số lượng ghế"
        //        });
        //    }
        //    else
        //    {
        //        if (soluong > count)
        //        {
        //            return BadRequest(new ApiResponse
        //            {
        //                Success = false,
        //                Message = "Số lượng ghế không đủ"
        //            });
        //        }
        //        else
        //        {

        //            //input đầu vào (ng dùng chọn ghế nào trong ghée trống)
        //            //hàm trả về các mã ghế ng ta chọn
        //            //load danh sách các ghế còn trống
        //            var ghe = (from g in _context.Ghes
        //                       where g.NhaKich == "NK000000002" && g.MaGhe.Contains(maghe)
        //                       select new
        //                       {
        //                           g.MaGhe,
        //                           g.NhaKich,
        //                           g.Hang,
        //                           g.Seat,
        //                           g.Status
        //                       }).ToList();

        //            //var result = ghe.Select(g=>new GheModel
        //            //{
        //            //    MaGhe=g.MaGhe,
        //            //    NhaKich=g.NhaKich,
        //            //    Hang=g.Hang,
        //            //    Seat=g.Seat,
        //            //    Status=g.Status
        //            //}).ToList();

        //            //List<GheModel> gheModels= new List<GheModel>();
        //            //gheModels.AddRange(result);


        //            //                 
        //        }
        //    }



        //    if (id != null || idtaikhoan != null)
        //    {
        //        model.MaKh = id;
        //        model.MaTk = idtaikhoan;
        //        var newVeId = await _VeRepo.Add(model);

        //        if (newVeId == "2")
        //        {
        //            return BadRequest(new ApiResponse
        //            {
        //                Success = false,
        //                Message = "Ban chua chon ghe"
        //            });
        //        }
        //        if (newVeId == "3")
        //        {
        //            return BadRequest(new ApiResponse
        //            {
        //                Success = false,
        //                Message = "Ban chua chon xuat chieu"
        //            });
        //        }
        //        else
        //        {
        //            var Ve = await _VeRepo.GetByID(newVeId);
        //            return Ve == null ? NotFound() : Ok(Ve);
        //        }

        //    }
        //    else
        //    {
        //        return BadRequest(new ApiResponse
        //        {
        //            Success = false,
        //            Message = "Bạn chưa đăng nhập"
        //        });
        //    }
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVe(string id,VeModel model)
        {
            await _VeRepo.Update(id, model);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteVe([FromRoute] string id,DeleteVeModel model)
        {
            model.TinhTrang = 0;
            await _VeRepo.XoaVe(id,model);
            return Ok();
        }
    }
}
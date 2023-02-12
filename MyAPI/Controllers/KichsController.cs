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
        private readonly IWebHostEnvironment _hostEnvironment;

        public KichsController(IKichRepository repo,MyDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _KichRepo = repo;
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }
        [NonAction]
        public string ma()
        {
            int result = _context.Kiches.Count() + 1;
            if (result >= 0 && result < 10)
                return "K00000000" + result;
            else if (result >= 10 && result < 100)
                return "K0000000" + result;
            else if (result >= 100 && result < 1000)
                return "K00000" + result;
            else if (result >= 1000 && result < 10000)
                return "K0000" + result;
            else if (result >= 10000 && result < 100000)
                return "K000" + result;
            else if (result >= 100000 && result < 1000000)
                return "K00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "K0" + result;
            else return "K" + result;
        }
        [Authorize]
        [HttpGet]
        [Route("KichCuaMoiNhakich")]
        public async Task<IActionResult> GetAllKichNK()
        {
            try
            {
                string role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value!;
                if(role.Equals("admin"))
                {
                    var kich = (from dd in _context.Kiches
                                where dd.NgayBd<=DateTime.Now && dd.NgayKt>=DateTime.Now
                                select new
                                {
                                    MaKich = dd.MaKich,
                                    MaNhaKich = dd.MaNhaKich,
                                    TenKich = dd.TenKich,
                                    MoTa = dd.MoTa,
                                    Image = dd.Image,
                                    NgayBD = dd.NgayBd,
                                    NgayKt = dd.NgayKt,
                                    TheLoai = dd.TheLoai,
                                    TrangThai = dd.TrangThai,
                                    //    ImageSrc =String.Format("{0}://{1}{2}/Images/{3}",Request.Scheme,Request.Host,Request.PathBase,dd.Image)
                                }).ToList();
                    return Ok(kich);
                }   
                else if(role.Equals("Quản lý"))
                {
                    string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                    var mank = (from nk in _context.Nhakiches
                                where nk.MaNhaKich == idtaikhoan
                                select nk.MaNhaKich).SingleOrDefault()!.ToString();
                    var kich = (from dd in _context.Kiches
                                where dd.MaNhaKich!.Equals("" + mank + "")
                                select new
                                {
                                    MaKich = dd.MaKich,
                                    MaNhaKich = dd.MaNhaKich,
                                    TenKich = dd.TenKich,
                                    MoTa = dd.MoTa,
                                    Image = dd.Image,
                                    NgayBD = dd.NgayBd,
                                    NgayKt = dd.NgayKt,
                                    TheLoai = dd.TheLoai,
                                    TrangThai = dd.TrangThai,
 //                                   ImageSrc =String.Format("{0}://{1}{2}/Images/{3}",Request.Scheme,Request.Host,Request.PathBase,dd.Image)
                                }).ToList();
                    return Ok(kich);
                }
                return Ok();
               
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllKich()
        {

            var kich = (from dd in _context.Kiches
                        where dd.TrangThai == 1
                        select new
                        {
                            MaKich = dd.MaKich,
                            MaNhaKich = dd.MaNhaKich,
                            TenKich = dd.TenKich,
                            MoTa = dd.MoTa,
                            Image = dd.Image,
                            NgayBD = dd.NgayBd,
                            NgayKt = dd.NgayKt,
                            TheLoai = dd.TheLoai,
                            TrangThai = dd.TrangThai,
                            //                                   ImageSrc =String.Format("{0}://{1}{2}/Images/{3}",Request.Scheme,Request.Host,Request.PathBase,dd.Image)
                        }).ToList();
            return Ok(kich);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetKichByID(string id)
        {
            var Kich= await _KichRepo.GetByID(id);
            return  Kich==null ?NotFound():Ok(Kich);
        }
        [HttpGet("TimKich")]
        public async Task<IActionResult> GetKichByIdvaTen(string search)
        {
            var Kich = from x in _context.Kiches
                       where x.MaKich.Equals(search) || x.TenKich.Contains(search)
                       select new
                       {
                           MaKich = x.MaKich,
                           MaNhaKich = x.MaNhaKich,
                           MoTa = x.MoTa,
                           TenKich = x.TenKich,
                           TheLoai = x.TheLoai,
                           Image = x.Image,
                           NgayBd = x.NgayBd,
                           NgayKt = x.NgayKt,
                           TrangThai = x.TrangThai
                       };
            return Ok(Kich);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewKich(KichModel model)
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();

                // var mank1 = _context.Nhakiches.Select(x => x.TenNhaKich == idtaikhoan);
                model.MaNhaKich = mank;
                //             model.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, image);
                var newKichId = await _KichRepo.Add(model);
                if(newKichId==null)
                {
                    return BadRequest(new ApiResponse
                    {
                        Message = "Ngày bắt đầu phải trước ngày kết thúc",
                        Success = false
                    });
                }  
                else
                {
                    var Kich = await _KichRepo.GetByID(newKichId);
                    return Kich == null ? NotFound() : Ok(Kich);
                }    
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKich( string id,  KichModel model)
        {
           
            if(model.NgayBd>model.NgayKt)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Ngày bắt đầu phải trước ngày kết thúc"
                });
            }    
            else
            {
                var ktud = (from k in _context.Kiches
                            join xc in _context.Xuatchieus on k.MaKich equals xc.MaKich
                            where k.MaKich.Equals("" + id + "")
                            select k.MaKich).Count();
                //ktra kịch đã có xuất chiếu chưa
                if (ktud > 0)
                {
                    var ktdl = (from k in _context.Kiches
                                join xc in _context.Xuatchieus on k.MaKich equals xc.MaKich
                                where k.MaKich.Equals("" + id + "") && model.NgayBd <= xc.NgayChieu && model.NgayKt >= xc.NgayChieu
                                select k.MaKich).Count();
                    //ktra xem ngày bd sau khi update có trong khoảng thời gian chiếu ko
                    if (ktdl > 0)
                    {
                        //var xc1 = (from k in _context.Kiches
                        //          join xc in _context.Xuatchieus on k.MaKich equals xc.MaKich
                        //          where k.MaKich.Equals("" + id + "")
                        //          select xc.NgayChieu).ToString();
                        //DateTime myDate = DateTime.ParseExact(xc1!, "yyyy-MM-dd HH:mm:ss,fff",
                        //                   System.Globalization.CultureInfo.InvariantCulture);
                        //if (model.NgayBd<=myDate&&model.NgayKt>=myDate)
                        //{
                        
                        await _KichRepo.Update(id, model);
                        return Ok();
                        //  }   
                    }
                    else
                    {
                        return BadRequest(new ApiResponse
                        {
                            Success = false,
                            Message = "Kịch Đã có xuất chiếu , vui lòng  update vào đúng khung giờ"
                        });
                    }
                }
                //chưa có thì update thẳng
                else
                {
                    
                    await _KichRepo.Update(id, model);
                    return Ok();
                }
            }                
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
        public async Task<IActionResult> DeleteKich( string id)
        {
            var ktdl = (from k in _context.Kiches
                        join xc in _context.Xuatchieus on k.MaKich equals xc.MaKich
                        where k.MaKich.Equals("" + id + "") && xc.NgayChieu >= DateTime.Now
                        select k.MaKich).Count();
            if(ktdl > 0)
            {
                return Ok(new ApiResponse
                {
                    Success=false,
                    Message="Đã có lịch diễn, không thể xoá"
                });
            }  
            else
            {
                await _KichRepo.Delete(id);
                return Ok();
            }               
        }
        [Route("Search")]
        [HttpGet]
        public IActionResult GetallKichs(string search)
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
        //[NonAction]       
        //public async Task<string> SaveImage(IFormFile imageFile)
        //{
        //    string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
        //    imageName= imageName+DateTime.Now.ToString("yymmssfff")+Path.GetExtension(imageFile.FileName);
        //    var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
        //    using(var fileStream = new FileStream(imagePath,FileMode.Create))
        //    {
        //       await imageFile.CopyToAsync(fileStream);
        //    }
        //    return imageName;
        //}
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _hostEnvironment.ContentRootPath + "/Images/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if(System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
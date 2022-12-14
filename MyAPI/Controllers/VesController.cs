using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Collections.Immutable;
using System.Data;
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
        public async Task<IActionResult> AddNewVe(VeModel model,int soluong)
        {
            
            string id = HttpContext.User.FindFirst(ClaimTypes.Name)?.Value!;
            string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
            var select = from ghes in _context.Ghes
                         where ghes.Status == 0 && ghes.NhaKich== "NK000000002"
                         select ghes.MaGhe;
            int count = select.Count();
            if (soluong == null || soluong == 0)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Bạn chưa chọn số lượng ghế"
                });
            }
            else
            {
                if (soluong > count)
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Số lượng ghế không đủ"
                    });
                }
                else
                {
                    var ghe = (from g in _context.Ghes
                              where g.NhaKich== "NK000000002"
                              select new 
                              { 
                                 g.MaGhe,
                                 g.NhaKich,
                                 g.Hang,
                                 g.Seat,
                                 g.Status
                              }).ToList();
                    var result = ghe.Select(g=>new GheModel
                    {
                        MaGhe=g.MaGhe,
                        NhaKich=g.NhaKich,
                        Hang=g.Hang,
                        Seat=g.Seat,
                        Status=g.Status
                    }).ToList();
                    
                    List<GheModel> gheModels= new List<GheModel>();
                    gheModels.AddRange(result);
                    for(int i=0; i<soluong; i++)
                    {
                        
                    }    
 //                 
                }
            }



            if (id!=null || idtaikhoan!=null)
            {
                model.MaKh = id;
                model.MaTk = idtaikhoan;
                var newVeId = await _VeRepo.Add(model);

                if (newVeId == "2")
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Ban chua chon ghe"
                    });
                }
                if (newVeId == "3")
                {
                    return BadRequest(new ApiResponse
                    {
                        Success = false,
                        Message = "Ban chua chon xuat chieu"
                    });
                }
                else
                {
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVe(string id,VeModel model)
        {
            await _VeRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVe([FromRoute] string id)
        {
            await _VeRepo.Delete(id);
            return Ok();
        }
    }
}
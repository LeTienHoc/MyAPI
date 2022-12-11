using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatvesController : ControllerBase
    {
        //private readonly IVeRepository _verepo;
        //private readonly IKhachhangRepository _khrepo;
        //private readonly IGheRepository _gherepo;
        //private readonly IXuatchieuRepository _xcrepo;
        //private readonly MyDbContext _context;

        //public DatvesController(IVeRepository verepo,IKhachhangRepository khrepo,IGheRepository gherepo,IXuatchieuRepository xcrepo,MyDbContext context)
        //{
        //    _verepo = verepo;
        //    _khrepo = khrepo;
        //    _gherepo = gherepo;
        //    _xcrepo = xcrepo;
        //    _context = context;
        //}
        //public ActionResult<List<KhachhangModel>> FindKh()
        //{
        //    return ()
        //}
        //public async Task<KhachhangModel> findkh(string kh)
        //{
        //    var khachhang = await _khrepo.GetByID(kh);
        //    return khachhang;
        //}
        //public async Task<GheModel> findGhe(string ghe)
        //{
        //    var ghe1 = await _gherepo.GetByID(ghe);
        //    return ghe1;
        //}
        //public async Task<XuatchieuModel> findXC(string xc)
        //{
        //    var xuatchieu = await _xcrepo.GetByID(xc);
        //    return xuatchieu;
        //}
        //public string ma()
        //{
        //    int result = _context.Taikhoans.Count() + 1;
        //    if (result >= 0 && result < 10)
        //        return "TK00000000" + result;
        //    else if (result >= 10 && result < 100)
        //        return "TK0000000" + result;
        //    else if (result >= 100 && result < 1000)
        //        return "TK00000" + result;
        //    else if (result >= 1000 && result < 10000)
        //        return "TK0000" + result;
        //    else if (result >= 10000 && result < 100000)
        //        return "TK000" + result;
        //    else if (result >= 100000 && result < 1000000)
        //        return "TK00" + result;
        //    else if (result >= 1000000 && result < 10000000)
        //        return "TK0" + result;
        //    else return "TK" + result;
        //}
        //[HttpPost("Datve")]
        //public async Task<IActionResult> DatVe(VeModel ve)
        //{
        //    var datve = new Ve
        //    {
        //        MaKh =findkh(ve.MaKh),
        //    };
        //    return Ok();

        //}
    }
}

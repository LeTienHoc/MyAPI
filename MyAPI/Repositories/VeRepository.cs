using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MyAPI.Data;
using MyAPI.Models;
using System.Security.Claims;

namespace MyAPI.Repositories
{
    public class VeRepository : IVeRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public VeRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Ves.Count() + 1;
            if (result >= 0 && result < 10)
                return "V00000000" + result;
            else if (result >= 10 && result < 100)
                return "V0000000" + result;
            else if (result >= 100 && result < 1000)
                return "V00000" + result;
            else if (result >= 1000 && result < 10000)
                return "V0000" + result;
            else if (result >= 10000 && result < 100000)
                return "V000" + result;
            else if (result >= 100000 && result < 1000000)
                return "V00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "V0" + result;
            else return "V" + result;
        }
        public string findkh()
        {

            //return int.Parse(this._context.Taikhoans.Claims.First(i => i.Type == "UserId").Value);

            string loi = "";
            //var khach = _context.Khachhangs.Where(o => o.MaKh == kh);
            //if(khach.Any())
            //{
            //    var khachhang = _context.Khachhangs.FindAsync(kh).Result.MaKh;
            //    return khachhang;
            //}
            return loi;



        }
        public string findxc(string xc)
        {
            var xuatchieu = _context.Xuatchieus.FindAsync(xc).Result!.MaXc;
            return xuatchieu;
        }
        public string findghe(string g,int sl)
        {
            for (int i=1;i<=sl;i++)
            {
                var ghe = _context.Ghes.FindAsync(g).Result!.MaGhe;
                return ghe;
            }    
            return g;
            
        }
        public async Task<string> Add(VeModel Ve)
        {
            

            var newVe = _mapper.Map<Ve>(Ve);
            //if (findkh(Ve.MaKh) == "")
            //{
            //    return null;
            //}
            newVe.MaVe = ma();
            newVe.MaXc = findxc(Ve.MaXc);
            newVe.MaGhe = findghe(Ve.MaGhe,3);


            _context.Ves!.Add(newVe);
            await _context.SaveChangesAsync();


            return newVe.MaVe;
        }

        public async Task Delete(string id)
        {
            var deleteVe = _context.Ves!.SingleOrDefault(b=>b.MaVe==id);
            if(deleteVe!=null)
            {
                _context.Ves.Remove(deleteVe);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<VeModel>> GetAll()
        {
            var Ves = await _context.Ves!.ToListAsync();
            return _mapper.Map<List<VeModel>>(Ves);
        }

        public async Task<VeModel> GetByID(string id)
        {
            var Ve = await _context.Ves!.FindAsync(id);
            return _mapper.Map<VeModel>(Ve);
        }

        public async Task Update(string id, VeModel Ve)
        {
            if(id == Ve.MaVe)
            {
                var updateVe = _mapper.Map<Ve>(Ve);
                _context.Ves!.Update(updateVe);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

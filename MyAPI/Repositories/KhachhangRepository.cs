using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class KhachhangRepository : IKhachhangRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public KhachhangRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> Add(KhachhangModel Khachhang)
        {
            var newKhachhang = _mapper.Map<Khachhang>(Khachhang);
            //string ma = "";
            //var select = await _context.Khachhangs.ToListAsync();
            //int count = select.Count();
            //if (count <= 0)
            //{
            //    ma = "DD001";
            //}
            //else
            //{
            //    int k;
            //    ma = "DD";
            //    int h;
            //    h = count - 1;
            //    k = Convert.ToInt32((h).ToString().Substring(2, 3));
            //    k = k + 1;
            //    if (k < 10)
            //    {
            //        ma = ma + "00";
            //    }
            //    else if (k < 100)
            //    {
            //        ma = ma + "0";
            //    }
            //    ma = ma + k.ToString();
            //}
            //newKhachhang.MaKhachhang = ma;
            _context.Khachhangs!.Add(newKhachhang);
            await _context.SaveChangesAsync();

            return newKhachhang.MaKh;
        }

        public async Task Delete(string id)
        {
            var deleteKhachhang = _context.Khachhangs!.SingleOrDefault(b=>b.MaKh == id);
            if(deleteKhachhang!=null)
            {
                _context.Khachhangs.Remove(deleteKhachhang);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<KhachhangModel>> GetAll()
        {
            var Khachhangs = await _context.Khachhangs!.ToListAsync();
            return _mapper.Map<List<KhachhangModel>>(Khachhangs);
        }

        public async Task<KhachhangModel> GetByID(string id)
        {
            var Khachhang = await _context.Khachhangs!.FindAsync(id);
            return _mapper.Map<KhachhangModel>(Khachhang);
        }

        public async Task Update(string id, KhachhangModel Khachhang)
        {
            if(id == Khachhang.MaKh)
            {
                var updateKhachhang = _mapper.Map<Khachhang>(Khachhang);
                _context.Khachhangs!.Update(updateKhachhang);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

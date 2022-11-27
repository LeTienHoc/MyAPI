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
        public string ma()
        {
            int result = _context.Khachhangs.Count() + 1;
            if (result >= 0 && result < 10)
                return "KH00000000" + result;
            else if (result >= 10 && result < 100)
                return "KH0000000" + result;
            else if (result >= 100 && result < 1000)
                return "KH00000" + result;
            else if (result >= 1000 && result < 10000)
                return "KH0000" + result;
            else if (result >= 10000 && result < 100000)
                return "KH000" + result;
            else if (result >= 100000 && result < 1000000)
                return "KH00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "KH0" + result;
            else return "KH" + result;
        }
        public async Task<string> Add(KhachhangModel Khachhang)
        {
            var newKhachhang = _mapper.Map<Khachhang>(Khachhang);
            
            newKhachhang.MaKh = ma();
            if(newKhachhang.MatKhau == newKhachhang.ConfirmMatKhau)
            {
                _context.Khachhangs!.Add(newKhachhang);
                await _context.SaveChangesAsync();
            }    
            

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

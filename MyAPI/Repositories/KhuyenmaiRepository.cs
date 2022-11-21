using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class KhuyenmaiRepository : IKhuyenmaiRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public KhuyenmaiRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Khuyenmais.Count() + 1;
            if (result >= 0 && result < 10)
                return "KM00000000" + result;
            else if (result >= 10 && result < 100)
                return "KM0000000" + result;
            else if (result >= 100 && result < 1000)
                return "KM00000" + result;
            else if (result >= 1000 && result < 10000)
                return "KM0000" + result;
            else if (result >= 10000 && result < 100000)
                return "KM000" + result;
            else if (result >= 100000 && result < 1000000)
                return "KM00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "KM0" + result;
            else return "KM" + result;
        }
        public async Task<string> Add(KhuyenmaiModel Khuyenmai)
        {
            var newKhuyenmai = _mapper.Map<Khuyenmai>(Khuyenmai);
            //string ma = "";
            //var select = await _context.Khuyenmais.ToListAsync();
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
            newKhuyenmai.MaKm = ma();
            _context.Khuyenmais!.Add(newKhuyenmai);
            await _context.SaveChangesAsync();

            return newKhuyenmai.MaKm;
        }

        public async Task Delete(string id)
        {
            var deleteKhuyenmai = _context.Khuyenmais!.SingleOrDefault(b=>b.MaKm==id);
            if(deleteKhuyenmai!=null)
            {
                _context.Khuyenmais.Remove(deleteKhuyenmai);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<KhuyenmaiModel>> GetAll()
        {
            var Khuyenmais = await _context.Khuyenmais!.ToListAsync();
            return _mapper.Map<List<KhuyenmaiModel>>(Khuyenmais);
        }

        public async Task<KhuyenmaiModel> GetByID(string id)
        {
            var Khuyenmai = await _context.Khuyenmais!.FindAsync(id);
            return _mapper.Map<KhuyenmaiModel>(Khuyenmai);
        }

        public async Task Update(string id, KhuyenmaiModel Khuyenmai)
        {
            if(id == Khuyenmai.MaKm)
            {
                var updateKhuyenmai = _mapper.Map<Khuyenmai>(Khuyenmai);
                _context.Khuyenmais!.Update(updateKhuyenmai);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

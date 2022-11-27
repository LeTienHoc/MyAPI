using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class LichchieuKhuyenmaiRepository : ILichchieuKhuyenmaiRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public LichchieuKhuyenmaiRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> Add(LichchieuKhuyenmaiModel LichchieuKhuyenmai)
        {
            var newLichchieuKhuyenmai = _mapper.Map<Lichchieu_Khuyenmai>(LichchieuKhuyenmai);
            //string ma = "";
            //var select = await _context.LichchieuKhuyenmais.ToListAsync();
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
            //newLichchieuKhuyenmai.MaLichchieuKhuyenmai = ma;
            _context.Lichchieu_Khuyenmais!.Add(newLichchieuKhuyenmai);
            await _context.SaveChangesAsync();

            return newLichchieuKhuyenmai.MaLichChieu;
        }

        public async Task Delete(string id)
        {
            var deleteLichchieuKhuyenmai = _context.Lichchieu_Khuyenmais!.SingleOrDefault(b=>b.MaLichChieu==id);
            if(deleteLichchieuKhuyenmai!=null)
            {
                _context.Lichchieu_Khuyenmais.Remove(deleteLichchieuKhuyenmai);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<LichchieuKhuyenmaiModel>> GetAll()
        {
            var LichchieuKhuyenmais = await _context.Lichchieu_Khuyenmais!.ToListAsync();
            return _mapper.Map<List<LichchieuKhuyenmaiModel>>(LichchieuKhuyenmais);
        }

        public async Task<LichchieuKhuyenmaiModel> GetByID(string id)
        {
            var LichchieuKhuyenmai = await _context.Lichchieu_Khuyenmais!.FindAsync(id);
            return _mapper.Map<LichchieuKhuyenmaiModel>(LichchieuKhuyenmai);
        }

        public async Task Update(string id, LichchieuKhuyenmaiModel LichchieuKhuyenmai)
        {
            if(id == LichchieuKhuyenmai.MaLichChieu)
            {
                var updateLichchieuKhuyenmai = _mapper.Map<Lichchieu_Khuyenmai>(LichchieuKhuyenmai);
                _context.Lichchieu_Khuyenmais!.Update(updateLichchieuKhuyenmai);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

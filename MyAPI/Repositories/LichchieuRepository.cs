using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class LichchieuRepository : ILichchieuRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public LichchieuRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> Add(LichchieuModel Lichchieu)
        {
            var newLichchieu = _mapper.Map<Lichchieu>(Lichchieu);

            if(newLichchieu.NgayBd<newLichchieu.NgayKt)
            {
                _context.Lichchieus!.Add(newLichchieu);
                await _context.SaveChangesAsync();
            }    
            else
            {
                TempDataAttribute["Alert"] = "";
            }    
            //string ma = "";
            //var select = await _context.Lichchieus.ToListAsync();
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
            //newLichchieu.MaLichchieu = ma;
            

            return newLichchieu.MaLichChieu;
        }

        public async Task Delete(string id)
        {
            var deleteLichchieu = _context.Lichchieus!.SingleOrDefault(b=>b.MaLichChieu==id);
            if(deleteLichchieu!=null)
            {
                _context.Lichchieus.Remove(deleteLichchieu);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<LichchieuModel>> GetAll()
        {
            var Lichchieus = await _context.Lichchieus!.ToListAsync();
            return _mapper.Map<List<LichchieuModel>>(Lichchieus);
        }

        public async Task<LichchieuModel> GetByID(string id)
        {
            var Lichchieu = await _context.Lichchieus!.FindAsync(id);
            return _mapper.Map<LichchieuModel>(Lichchieu);
        }

        public async Task Update(string id, LichchieuModel Lichchieu)
        {
            if(id == Lichchieu.MaLichChieu)
            {
                var updateLichchieu = _mapper.Map<Lichchieu>(Lichchieu);
                _context.Lichchieus!.Update(updateLichchieu);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

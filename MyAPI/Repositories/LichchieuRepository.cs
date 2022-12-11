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
        public string ma()
        {
            int result = _context.Lichchieus.Count() + 1;
            if (result >= 0 && result < 10)
                return "LC00000000" + result;
            else if (result >= 10 && result < 100)
                return "LC0000000" + result;
            else if (result >= 100 && result < 1000)
                return "LC00000" + result;
            else if (result >= 1000 && result < 10000)
                return "LC0000" + result;
            else if (result >= 10000 && result < 100000)
                return "LC000" + result;
            else if (result >= 100000 && result < 1000000)
                return "LC00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "LC0" + result;
            else return "LC" + result;
        }
        public async Task<string> Add(LichchieuModel Lichchieu)
        {
            var newLichchieu = _mapper.Map<Lichchieu>(Lichchieu);

 //           if(newLichchieu.NgayBd<newLichchieu.NgayKt)
  //          {
                newLichchieu.MaLichChieu = ma();
                _context.Lichchieus!.Add(newLichchieu);
                await _context.SaveChangesAsync();
  //          }
//            else
//            {
  //              return null;
 //           }    
            


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

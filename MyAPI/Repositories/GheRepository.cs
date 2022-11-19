using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class GheRepository : IGheRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public GheRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> Add(GheModel Ghe)
        {
            var newGhe = _mapper.Map<Ghe>(Ghe);
            //string ma = "";
            //var select = await _context.Ghes.ToListAsync();
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
            //newGhe.MaGhe = ma;
            _context.Ghes!.Add(newGhe);
            await _context.SaveChangesAsync();

            return newGhe.MaGhe;
        }

        public async Task Delete(string id)
        {
            var deleteGhe = _context.Ghes!.SingleOrDefault(b=>b.MaGhe==id);
            if(deleteGhe!=null)
            {
                _context.Ghes.Remove(deleteGhe);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<GheModel>> GetAll()
        {
            var Ghes = await _context.Ghes!.ToListAsync();
            return _mapper.Map<List<GheModel>>(Ghes);
        }

        public async Task<GheModel> GetByID(string id)
        {
            var Ghe = await _context.Ghes!.FindAsync(id);
            return _mapper.Map<GheModel>(Ghe);
        }

        public async Task Update(string id, GheModel Ghe)
        {
            if(id == Ghe.MaGhe)
            {
                var updateGhe = _mapper.Map<Ghe>(Ghe);
                _context.Ghes!.Update(updateGhe);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

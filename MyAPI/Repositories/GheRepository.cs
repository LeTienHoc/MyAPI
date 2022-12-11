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
        public string ma()
        {
            int result = _context.Ghes.Count() + 1;
            if (result >= 0 && result < 10)
                return "G00000000" + result;
            else if (result >= 10 && result < 100)
                return "G0000000" + result;
            else if (result >= 100 && result < 1000)
                return "G00000" + result;
            else if (result >= 1000 && result < 10000)
                return "G0000" + result;
            else if (result >= 10000 && result < 100000)
                return "G000" + result;
            else if (result >= 100000 && result < 1000000)
                return "G00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "G0" + result;
            else return "G" + result;
        }
        public async Task<string> Add(GheModel Ghe)
        {
            var newGhe = _mapper.Map<Ghe>(Ghe);

            newGhe.MaGhe = ma();
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

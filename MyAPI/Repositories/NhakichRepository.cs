using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class NhakichRepository : INhakichRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public NhakichRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Nhakiches.Count() + 1;
            if (result >= 0 && result < 10)
                return "NK00000000" + result;
            else if (result >= 10 && result < 100)
                return "NK0000000" + result;
            else if (result >= 100 && result < 1000)
                return "NK00000" + result;
            else if (result >= 1000 && result < 10000)
                return "NK0000" + result;
            else if (result >= 10000 && result < 100000)
                return "NK000" + result;
            else if (result >= 100000 && result < 1000000)
                return "NK00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "NK0" + result;
            else return "NK" + result;
        }
        public async Task<string> Add(NhakichModel Nhakich)
        {
            var newNhakich = _mapper.Map<Nhakich>(Nhakich);
            
            newNhakich.MaNhaKich = ma();
            _context.Nhakiches!.Add(newNhakich);
            await _context.SaveChangesAsync();

            return newNhakich.MaNhaKich;
        }

        public async Task Delete(string id)
        {
            var deleteNhakich = _context.Nhakiches!.SingleOrDefault(b=>b.MaNhaKich==id);
            if(deleteNhakich!=null)
            {
                _context.Nhakiches.Remove(deleteNhakich);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<NhakichModel>> GetAll()
        {
            var Nhakichs = await _context.Nhakiches!.ToListAsync();
            return _mapper.Map<List<NhakichModel>>(Nhakichs);
        }

        public async Task<NhakichModel> GetByID(string id)
        {
            var Nhakich = await _context.Nhakiches!.FindAsync(id);
            return _mapper.Map<NhakichModel>(Nhakich);
        }

        public async Task Update(string id, NhakichModel Nhakich)
        {
            if(id == Nhakich.MaNhaKich)
            {
                var updateNhakich = _mapper.Map<Nhakich>(Nhakich);
                _context.Nhakiches!.Update(updateNhakich);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

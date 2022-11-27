using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class KichRepository : IKichRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public KichRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Kiches.Count() + 1;
            if (result >= 0 && result < 10)
                return "K00000000" + result;
            else if (result >= 10 && result < 100)
                return "K0000000" + result;
            else if (result >= 100 && result < 1000)
                return "K00000" + result;
            else if (result >= 1000 && result < 10000)
                return "K0000" + result;
            else if (result >= 10000 && result < 100000)
                return "K000" + result;
            else if (result >= 100000 && result < 1000000)
                return "K00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "K0" + result;
            else return "K" + result;
        }
        public async Task<string> Add(KichModel Kich)
        {
            var newKich = _mapper.Map<Kich>(Kich);
            if(newKich.NgayBd<newKich.NgayKt)
            {
                newKich.MaKich = ma();
                _context.Kiches!.Add(newKich);
                await _context.SaveChangesAsync();
            }   
            else
            {
                return null;
            }    
            

            return newKich.MaKich;
        }

        public async Task Delete(string id)
        {
            var deleteKich = _context.Kiches!.SingleOrDefault(b=>b.MaKich==id);
            if(deleteKich!=null)
            {
                _context.Kiches.Remove(deleteKich);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<KichModel>> GetAll()
        {
            var Kichs = await _context.Kiches!.ToListAsync();
            return _mapper.Map<List<KichModel>>(Kichs);
        }

        public async Task<KichModel> GetByID(string id)
        {
            var Kich = await _context.Kiches!.FindAsync(id);
            return _mapper.Map<KichModel>(Kich);
        }

        public async Task Update(string id, KichModel Kich)
        {
            if(id == Kich.MaKich)
            {
                var updateKich = _mapper.Map<Kich>(Kich);
                if(updateKich.NgayBd<updateKich.NgayKt)
                {
                    _context.Kiches!.Update(updateKich);
                    await _context.SaveChangesAsync();
                }    
            }    
        }

    }
}

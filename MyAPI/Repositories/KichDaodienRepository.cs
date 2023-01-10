using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class KichDaodienRepository : IKichDaodienRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public KichDaodienRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> Add(KichDaodienModel KichDaodien)
        {
            var newKichDaodien = _mapper.Map<KichDaodien>(KichDaodien);
            var dvkich = (from k in _context.Kiches
                          join v in _context.Daodiens on k.MaNhaKich equals v.MaNhaKich
                          join nk in _context.Nhakiches on k.MaNhaKich equals nk.MaNhaKich
                          where v.MaDaoDien == KichDaodien.MaDaodien && k.MaKich==KichDaodien.MaKich
                          select k.MaKich).SingleOrDefault()?.ToString();
            if (dvkich == KichDaodien.MaKich)
            {
                _context.KichDaodiens!.Add(newKichDaodien);
                await _context.SaveChangesAsync();
        }
            else
            {
                return null;
            }    
            return newKichDaodien.MaDaodien;
        }

        public async Task Delete(string id)
        {
            var deleteKichDaodien = _context.KichDaodiens!.SingleOrDefault(b=>b.MaDaodien==id);
            if(deleteKichDaodien!=null)
            {
                _context.KichDaodiens.Remove(deleteKichDaodien);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<KichDaodienModel>> GetAll()
        {
            var KichDaodiens = await _context.KichDaodiens!.ToListAsync();
            return _mapper.Map<List<KichDaodienModel>>(KichDaodiens);
        }

        public async Task<KichDaodienModel> GetByID(string id)
        {
            var KichDaodien = await _context.KichDaodiens!.FindAsync(id);
            return _mapper.Map<KichDaodienModel>(KichDaodien);
        }

        public async Task Update(string id, KichDaodienModel KichDaodien)
        {
            if(id == KichDaodien.MaDaodien)
            {
                var updateKichDaodien = _mapper.Map<KichDaodien>(KichDaodien);
                _context.KichDaodiens!.Update(updateKichDaodien);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

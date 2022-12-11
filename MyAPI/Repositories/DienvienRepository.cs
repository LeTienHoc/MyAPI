using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class DienvienRepository : IDienvienRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public DienvienRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string ma()
        {
            int result = _context.Dienviens.Count() + 1;
            if (result >= 0 && result < 10)
                return "DV00000000" + result;
            else if (result >= 10 && result < 100)
                return "DV0000000" + result;
            else if (result >= 100 && result < 1000)
                return "DV00000" + result;
            else if (result >= 1000 && result < 10000)
                return "DV0000" + result;
            else if (result >= 10000 && result < 100000)
                return "DV000" + result;
            else if (result >= 100000 && result < 1000000)
                return "DV00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "DV0" + result;
            else return "DV" + result;
        }
        public async Task<string> Add(DienvienModel Dienvien)
        {
            var newDienvien = _mapper.Map<Dienvien>(Dienvien);
            
            newDienvien.MaDienVien = ma();
            _context.Dienviens!.Add(newDienvien);
            await _context.SaveChangesAsync();

            return newDienvien.MaDienVien;
        }

        public async Task Delete(string id)
        {
            var deleteDienvien = _context.Dienviens!.SingleOrDefault(b=>b.MaDienVien==id);
            if(deleteDienvien!=null)
            {
                _context.Dienviens.Remove(deleteDienvien);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<DienvienModel>> GetAll()
        {
            var Dienviens = await _context.Dienviens!.ToListAsync();
            return _mapper.Map<List<DienvienModel>>(Dienviens);
        }

        public async Task<DienvienModel> GetByID(string id)
        {
            var Dienvien = await _context.Dienviens!.FindAsync(id);
            return _mapper.Map<DienvienModel>(Dienvien);
        }

        public async Task Update(string id, DienvienModel Dienvien)
        {
            if(id == Dienvien.MaDienVien)
            {
                var updateDienvien = _mapper.Map<Dienvien>(Dienvien);
                _context.Dienviens!.Update(updateDienvien);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

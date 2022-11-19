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
        public async Task<string> Add(DienvienModel Dienvien)
        {
            var newDienvien = _mapper.Map<Dienvien>(Dienvien);
            //string ma = "";
            //var select = await _context.Dienviens.ToListAsync();
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
            //newDienvien.MaDienvien = ma;
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

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class KichDienvienRepository : IKichDienvienRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public KichDienvienRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> Add(KichDienvienModel KichDienvien)
        {
            var newKichDienvien = _mapper.Map<KichDienvien>(KichDienvien);
            //string ma = "";
            //var select = await _context.KichDienviens.ToListAsync();
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
            //newKichDienvien.MaKichDienvien = ma;
            _context.KichDienviens!.Add(newKichDienvien);
            await _context.SaveChangesAsync();

            return newKichDienvien.MaDienVien;
        }

        public async Task Delete(string id)
        {
            var deleteKichDienvien = _context.KichDienviens!.SingleOrDefault(b=>b.MaDienVien==id);
            if(deleteKichDienvien!=null)
            {
                _context.KichDienviens.Remove(deleteKichDienvien);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<KichDienvienModel>> GetAll()
        {
            var KichDienviens = await _context.KichDienviens!.ToListAsync();
            return _mapper.Map<List<KichDienvienModel>>(KichDienviens);
        }

        public async Task<KichDienvienModel> GetByID(string id)
        {
            var KichDienvien = await _context.KichDienviens!.FindAsync(id);
            return _mapper.Map<KichDienvienModel>(KichDienvien);
        }

        public async Task Update(string id, KichDienvienModel KichDienvien)
        {
            if(id == KichDienvien.MaDienVien)
            {
                var updateKichDienvien = _mapper.Map<KichDienvien>(KichDienvien);
                _context.KichDienviens!.Update(updateKichDienvien);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

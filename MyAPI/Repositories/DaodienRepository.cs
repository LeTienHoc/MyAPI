using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class DaodienRepository : IDaodienRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public DaodienRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> Add(DaodienModel daodien)
        {
            var newDaodien = _mapper.Map<Daodien>(daodien);
            //string ma = "";
            //var select = await _context.Daodiens.ToListAsync();
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
            //newDaodien.MaDaoDien = ma;
            _context.Daodiens!.Add(newDaodien);
            await _context.SaveChangesAsync();

            return newDaodien.MaDaoDien;
        }

        public async Task Delete(string id)
        {
            var deleteDaodien = _context.Daodiens!.SingleOrDefault(b=>b.MaDaoDien==id);
            if(deleteDaodien!=null)
            {
                _context.Daodiens.Remove(deleteDaodien);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<DaodienModel>> GetAll()
        {
            var daodiens = await _context.Daodiens!.ToListAsync();
            return _mapper.Map<List<DaodienModel>>(daodiens);
        }

        public async Task<DaodienModel> GetByID(string id)
        {
            var daodien = await _context.Daodiens!.FindAsync(id);
            return _mapper.Map<DaodienModel>(daodien);
        }

        public async Task Update(string id, DaodienModel daodien)
        {
            if(id == daodien.MaDaoDien)
            {
                var updateDaodien = _mapper.Map<Daodien>(daodien);
                _context.Daodiens!.Update(updateDaodien);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

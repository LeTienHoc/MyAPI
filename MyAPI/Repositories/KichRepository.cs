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
        public async Task<string> Add(KichModel Kich)
        {
            var newKich = _mapper.Map<Kich>(Kich);
            //string ma = "";
            //var select = await _context.Kichs.ToListAsync();
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
            //newKich.MaKich = ma;
            _context.Kiches!.Add(newKich);
            await _context.SaveChangesAsync();

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
                _context.Kiches!.Update(updateKich);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

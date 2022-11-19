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
        public async Task<string> Add(NhakichModel Nhakich)
        {
            var newNhakich = _mapper.Map<Nhakich>(Nhakich);
            //string ma = "";
            //var select = await _context.Nhakichs.ToListAsync();
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
            //newNhakich.MaNhakich = ma;
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

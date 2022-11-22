using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class BinhluanRepository : IBinhluanRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public BinhluanRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string id()
        {
            int result = _context.Binhluans.Count() + 1;
            if (result >= 0 && result < 10)
                return "IS00000000" + result;
            else if (result >= 10 && result < 100)
                return "IS0000000" + result;
            else if (result >= 100 && result < 1000)
                return "IS00000" + result;
            else if (result >= 1000 && result < 10000)
                return "IS0000" + result;
            else if (result >= 10000 && result < 100000)
                return "IS000" + result;
            else if (result >= 100000 && result < 1000000)
                return "IS00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "IS0" + result;
            else return "IS" + result;
        }
        public async Task<string> Add(BinhluanModel Binhluan)
        {
            var newBinhluan = _mapper.Map<Binhluan>(Binhluan);
            //string ma = "";
            //var select = await _context.Binhluans.ToListAsync();
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
            newBinhluan.MaBl = id();
            _context.Binhluans!.Add(newBinhluan);
            await _context.SaveChangesAsync();

            return newBinhluan.MaBl;
        }

        public async Task Delete(string id)
        {
            var deleteBinhluan = _context.Binhluans!.SingleOrDefault(b=>b.MaBl==id);
            if(deleteBinhluan!=null)
            {
                _context.Binhluans.Remove(deleteBinhluan);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<BinhluanModel>> GetAll()
        {
            var Binhluans = await _context.Binhluans!.ToListAsync();
            return _mapper.Map<List<BinhluanModel>>(Binhluans);
        }

        public async Task<BinhluanModel> GetByID(string id)
        {
            var Binhluan = await _context.Binhluans!.FindAsync(id);
            return _mapper.Map<BinhluanModel>(Binhluan);
        }

        public async Task Update(string id, BinhluanModel Binhluan)
        {
            if(id == Binhluan.MaBl)
            {
                var updateBinhluan = _mapper.Map<Binhluan>(Binhluan);
                _context.Binhluans!.Update(updateBinhluan);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

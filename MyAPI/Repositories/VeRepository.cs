using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class VeRepository : IVeRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public VeRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Taikhoans.Count() + 1;
            if (result >= 0 && result < 10)
                return "V00000000" + result;
            else if (result >= 10 && result < 100)
                return "V0000000" + result;
            else if (result >= 100 && result < 1000)
                return "V00000" + result;
            else if (result >= 1000 && result < 10000)
                return "V0000" + result;
            else if (result >= 10000 && result < 100000)
                return "V000" + result;
            else if (result >= 100000 && result < 1000000)
                return "V00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "V0" + result;
            else return "V" + result;
        }
        public async Task<string> Add(VeModel Ve)
        {
            var newVe = _mapper.Map<Ve>(Ve);
            //string ma = "";
            //var select = await _context.Ves.ToListAsync();
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
            newVe.MaVe = ma();
            _context.Ves!.Add(newVe);
            await _context.SaveChangesAsync();

            return newVe.MaVe;
        }

        public async Task Delete(string id)
        {
            var deleteVe = _context.Ves!.SingleOrDefault(b=>b.MaVe==id);
            if(deleteVe!=null)
            {
                _context.Ves.Remove(deleteVe);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<VeModel>> GetAll()
        {
            var Ves = await _context.Ves!.ToListAsync();
            return _mapper.Map<List<VeModel>>(Ves);
        }

        public async Task<VeModel> GetByID(string id)
        {
            var Ve = await _context.Ves!.FindAsync(id);
            return _mapper.Map<VeModel>(Ve);
        }

        public async Task Update(string id, VeModel Ve)
        {
            if(id == Ve.MaVe)
            {
                var updateVe = _mapper.Map<Ve>(Ve);
                _context.Ves!.Update(updateVe);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

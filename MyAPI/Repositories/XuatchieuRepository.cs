using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class XuatchieuRepository : IXuatchieuRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public XuatchieuRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Xuatchieus.Count() + 1;
            if (result >= 0 && result < 10)
                return "XC00000000" + result;
            else if (result >= 10 && result < 100)
                return "XC0000000" + result;
            else if (result >= 100 && result < 1000)
                return "XC00000" + result;
            else if (result >= 1000 && result < 10000)
                return "XC0000" + result;
            else if (result >= 10000 && result < 100000)
                return "XC000" + result;
            else if (result >= 100000 && result < 1000000)
                return "XC00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "XC0" + result;
            else return "XC" + result;
        }
        public async Task<string> Add(XuatchieuModel Xuatchieu)
        {
            var newXuatchieu = _mapper.Map<Xuatchieu>(Xuatchieu);
            
                newXuatchieu.MaXc = ma();
                _context.Xuatchieus!.Add(newXuatchieu);
                await _context.SaveChangesAsync();
            //}    
            

            return newXuatchieu.MaXc;
        }

        public async Task Delete(string id)
        {
            var deleteXuatchieu = _context.Xuatchieus!.SingleOrDefault(b=>b.MaXc==id);
            if(deleteXuatchieu!=null)
            {
                _context.Xuatchieus.Remove(deleteXuatchieu);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<XuatchieuModel>> GetAll()
        {
            var Xuatchieus = await _context.Xuatchieus!.ToListAsync();
            return _mapper.Map<List<XuatchieuModel>>(Xuatchieus);
        }

        public async Task<XuatchieuModel> GetByID(string id)
        {
            var Xuatchieu = await _context.Xuatchieus!.FindAsync(id);
            return _mapper.Map<XuatchieuModel>(Xuatchieu);
        }

        public async Task Update(string id, XuatchieuModel Xuatchieu)
        {
            if(id == Xuatchieu.MaXc)
            {
                var updateXuatchieu = _mapper.Map<Xuatchieu>(Xuatchieu);
                _context.Xuatchieus!.Update(updateXuatchieu);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

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
        public async Task<string> Add(XuatchieuModel Xuatchieu)
        {
            var newXuatchieu = _mapper.Map<Xuatchieu>(Xuatchieu);
            //string ma = "";
            //var select = await _context.Xuatchieus.ToListAsync();
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
            //newXuatchieu.MaXuatchieu = ma;
            _context.Xuatchieus!.Add(newXuatchieu);
            await _context.SaveChangesAsync();

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

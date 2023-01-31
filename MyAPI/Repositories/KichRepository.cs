using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MySqlConnector;
using System.Collections.Immutable;
using System.Data;
using System.Drawing;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyAPI.Repositories
{
    public class KichRepository : IKichRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;
        public static int PAGE_SIZE { get; set; } = 5;

        public KichRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Kiches.Count() + 1;
            if (result >= 0 && result < 10)
                return "K00000000" + result;
            else if (result >= 10 && result < 100)
                return "K0000000" + result;
            else if (result >= 100 && result < 1000)
                return "K00000" + result;
            else if (result >= 1000 && result < 10000)
                return "K0000" + result;
            else if (result >= 10000 && result < 100000)
                return "K000" + result;
            else if (result >= 100000 && result < 1000000)
                return "K00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "K0" + result;
            else return "K" + result;
        }

        public async Task<string> Add(KichModel Kich)
        {
            var newKich = _mapper.Map<Kich>(Kich);
            if(newKich.NgayBd<newKich.NgayKt)
            {
                newKich.MaKich = ma();
                _context.Kiches!.Add(newKich);
                await _context.SaveChangesAsync();
            }   
            else
            {
                return null;
            }    
            

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
            var Kichs = await _context.Kiches!/*.Where(t=>t.MaNhaKich!.Equals(""+manhakich+""))*/.ToListAsync();
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
                if(updateKich.NgayBd<updateKich.NgayKt)
                {                   
                    _context.Kiches!.Update(updateKich);
                    await _context.SaveChangesAsync();
                }    
            }    
        }
        public List<KichPageModel> Detail(string id)
        {
            var kichdienvien = (from k1 in _context.Kiches
                                join dv1 in _context.KichDienviens on k1.MaKich equals dv1.MaKich
                                join v1 in _context.Dienviens on dv1.MaDienVien equals v1.MaDienVien
                                join kd1 in _context.KichDaodiens on k1.MaKich equals kd1.MaKich
                                join dd1 in _context.Daodiens on kd1.MaDaodien equals dd1.MaDaoDien
                                where k1.MaKich==""+id+""
                                select new
                                {
                                    k1.MaKich,
                                    v1.TenDienVien,
                                    k1.TenKich,
                                    k1.TrangThai,
                                    k1.MoTa,
                                    dd1.TenDaoDien,
                                    k1.TheLoai,
                                    k1.Image,
                                    k1.NgayBd,
                                    k1.NgayKt
                                }).ToList();
            var group = (from k in kichdienvien
                         group k by k.MaKich into k2
                         select
                         new
                         {
                             k2.Key,
                             MoTa = k2.Select(p => p.MoTa).FirstOrDefault(),
                             DaoDien = string.Join(',', k2.Select(p => p.TenDaoDien).ToList().Distinct()),
                             TenKich = k2.Select(p => p.TenKich).FirstOrDefault(),
                             TheLoai = k2.Select(p => p.TheLoai).FirstOrDefault(),
                             Image = k2.Select(p => p.Image).FirstOrDefault(),
                             NgayBd = k2.Select(p => p.NgayBd).FirstOrDefault(),
                             NgayKt = k2.Select(p => p.NgayKt).FirstOrDefault(),
                             DienVien = string.Join(',', k2.Select(p => p.TenDienVien).ToList().Distinct()),

                         }).ToList()!.Select(x => new
                         {
                             MoTa = x.MoTa,
                             DaoDien = x.DaoDien,
                             TenKich = x.TenKich,
                             TheLoai = x.TheLoai,
                             Image = x.Image,
                             NgayBd = x.NgayBd,
                             NgayKt = x.NgayKt,
                             DienVien = x.DienVien,
                         });
            var result = group.Select(k => new KichPageModel
            {
                MoTa = k.MoTa,
                DaoDien = k.DaoDien,
                Image = k.Image,
                TenKich = k.TenKich,
                TheLoai = k.TheLoai,
                NgayBd = k.NgayBd,
                NgayKt = k.NgayKt,
                DienVien = k.DienVien
            });

            return result.ToList();
        }

        public List<KichPageModel> Getallkichs(string search,int page=1)
        {

            //var allKichs = _context.Kiches.AsQueryable();
            //#region Filter
            //if (!string.IsNullOrEmpty(search))
            //{
                var kichdienvien = (from k1 in _context.Kiches
                                join dv1 in _context.KichDienviens on k1.MaKich equals dv1.MaKich
                                join v1 in _context.Dienviens on dv1.MaDienVien equals v1.MaDienVien
                                join kd1 in _context.KichDaodiens on k1.MaKich equals kd1.MaKich
                                join dd1 in _context.Daodiens on kd1.MaDaodien equals dd1.MaDaoDien
                                where k1.TrangThai==1 && k1.TenKich.Contains(search)
                                select new
                                {
                                    k1.MaKich,
                                    v1.TenDienVien,
                                    k1.TenKich,
                                    k1.TrangThai,
                                    k1.MoTa,
                                    dd1.TenDaoDien,
                                    k1.TheLoai,
                                    k1.Image,
                                    k1.NgayBd,
                                    k1.NgayKt
                                }).ToList();
            var group = (from k in kichdienvien
                         group k by k.MaKich into k2
                         select
                         new
                         {
                             k2.Key,
                             MoTa = k2.Select(p => p.MoTa).FirstOrDefault(),
                             DaoDien = string.Join(',',k2.Select(p=>p.TenDaoDien).ToList().Distinct()),
                             TenKich = k2.Select(p => p.TenKich).FirstOrDefault(),
                             TheLoai = k2.Select(p => p.TheLoai).FirstOrDefault(),
                             Image = k2.Select(p => p.Image).FirstOrDefault(),
                             NgayBd = k2.Select(p => p.NgayBd).FirstOrDefault(),
                             NgayKt = k2.Select(p => p.NgayKt).FirstOrDefault(),
                             DienVien = string.Join(',', k2.Select(p => p.TenDienVien).ToList().Distinct()),

                         }).ToList()!.Select(x => new
                         {
                             MoTa = x.MoTa,
                             DaoDien = x.DaoDien,
                             TenKich = x.TenKich,
                             TheLoai = x.TheLoai,
                             Image = x.Image,
                             NgayBd = x.NgayBd,
                             NgayKt = x.NgayKt,
                             DienVien = x.DienVien,
                         });
            #region Paging
            group = group.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            #endregion
            var result = group.Select(k => new KichPageModel
            {
                MoTa = k.MoTa,
                DaoDien = k.DaoDien,
                Image = k.Image,
                TenKich = k.TenKich,
                TheLoai = k.TheLoai,
                NgayBd = k.NgayBd,
                NgayKt = k.NgayKt,
                DienVien = k.DienVien
            });
            
            return result.ToList();




        }

        public async Task DuyetKich(string id,UpdateModel model)
        {
            var updateKich = _mapper.Map<Kich>(model);
            updateKich = (from k in _context.Kiches
                          where k.MaKich == id
                          select k).SingleOrDefault();

            updateKich!.TrangThai = model.TrangThai;
            
            _context.SaveChanges();
                    
        }
    }
}

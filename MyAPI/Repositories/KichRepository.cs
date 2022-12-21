using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MySqlConnector;
using System.Drawing;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                if(updateKich.NgayBd<updateKich.NgayKt)
                {                   
                    _context.Kiches!.Update(updateKich);
                    await _context.SaveChangesAsync();
                }    
            }    
        }

        public List<KichPageModel> Getallkichs(string search)
        {

            //var allKichs = _context.Kiches.AsQueryable();
            //#region Filter
            //if (!string.IsNullOrEmpty(search))
            //{
            var alls = (from k in _context.Kiches
                        join dv in _context.KichDienviens on k.MaKich equals dv.MaKich
                        join v in _context.Dienviens on dv.MaDienVien equals v.MaDienVien
                        where k.TenKich.Contains(search) && k.TrangThai == 1 //|| k.TheLoai.Contains(search) && k.TrangThai == 1
                        //|| k.DaoDien.Contains(search) && k.TrangThai == 1 || v.TenDienVien.Contains(search) && k.TrangThai == 1
                        select new
                        {

                            MoTa = k.MoTa,
                            DaoDien = k.DaoDien,
                            Image = k.Image,
                            TenKich = k.TenKich,
                            TheLoai = k.TheLoai,
                            NgayBd = k.NgayBd,
                            NgayKt = k.NgayKt,
                            DienVien = (from kdv in _context.KichDienviens
                                        join dv in _context.Dienviens on kdv.MaDienVien equals dv.MaDienVien
                                        where k.TenKich.Contains(search) && k.TrangThai == 1 && k.MaKich==
                                        group k by k.MaKich into k1
                                        select string.Join(',', k1.Select(p => p.MaKich))),
                        }).ToList();


            /////////////////////////////////////////////

            //var all2s = (from k in _context.Kiches
            //             join dv in _context.KichDienviens on k.MaKich equals dv.MaKich
            //             join v in _context.Dienviens on dv.MaDienVien equals v.MaDienVien
            //             where k.TenKich.Contains(search) && k.TrangThai == 1
            //             group v by k into g
            //             select new
            //             {
            //                 k = g.Key,
            //                 MoTa = g.Key.MoTa,
            //                 DaoDien = g.Key.DaoDien,
            //                 TenKich = g.Key.TenKich,
            //                 TheLoai = g.Key.TheLoai,
            //                 Image = g.Key.Image,
            //                 NgayBd = g.Key.NgayBd,
            //                 NgayKt = g.Key.NgayKt,
            //                 TenDienViens = g.Select(tdv => tdv.TenDienVien),


            //             }).ToList().Select(x => new
            //             {
            //                 MoTa = x.MoTa,
            //                 DaoDien= x.DaoDien,
            //                 TenKich=x.TenKich,
            //                 TheLoai= x.TheLoai,
            //                 Image= x.Image,
            //                 NgayBd= x.NgayBd,
            //                 NgayKt= x.NgayKt,
            //                 TenDienViens = string.Join(",", x.TenDienViens),
            //             }).ToList();



            //  var ketqua = string.Join(alls,all2s);
            var result = alls.Select(k => new KichPageModel
                {
                    MoTa = k.MoTa,
                    DaoDien = k.DaoDien,
                    Image = k.Image,
                    TenKich = k.TenKich,
                    TheLoai = k.TheLoai,
                    NgayBd = k.NgayBd,
                    NgayKt = k.NgayKt,
                //DienVien = k.TenDienViens,
            });
                return result.ToList();
            //allKichs = allKichs.Where(k => k.TenKich.Contains(search)||k.TheLoai.Contains(search)
            //|| k.DaoDien.Contains(search));
            // }

            //#endregion
            //allKichs = allKichs.OrderBy(k => k.TenKich);

            //#region Sorting
            //#endregion


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

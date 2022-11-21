﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;

namespace MyAPI.Repositories
{
    public class TaikhoanRepository : ITaikhoanRepository
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public TaikhoanRepository(MyDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public string ma()
        {
            int result = _context.Taikhoans.Count() + 1;
            if (result >= 0 && result < 10)
                return "TK00000000" + result;
            else if (result >= 10 && result < 100)
                return "TK0000000" + result;
            else if (result >= 100 && result < 1000)
                return "TK00000" + result;
            else if (result >= 1000 && result < 10000)
                return "TK0000" + result;
            else if (result >= 10000 && result < 100000)
                return "TK000" + result;
            else if (result >= 100000 && result < 1000000)
                return "TK00" + result;
            else if (result >= 1000000 && result < 10000000)
                return "TK0" + result;
            else return "TK" + result;
        }
        public async Task<string> Add(TaikhoanModel Taikhoan)
        {
            var newTaikhoan = _mapper.Map<Taikhoan>(Taikhoan);
            //string ma = "";
            //var select = await _context.Taikhoans.ToListAsync();
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
            newTaikhoan.MaTk = ma();
            _context.Taikhoans!.Add(newTaikhoan);
            await _context.SaveChangesAsync();

            return newTaikhoan.MaTk;
        }

        public async Task Delete(string id)
        {
            var deleteTaikhoan = _context.Taikhoans!.SingleOrDefault(b=>b.MaTk==id);
            if(deleteTaikhoan!=null)
            {
                _context.Taikhoans.Remove(deleteTaikhoan);
                await _context.SaveChangesAsync();
            }    
        }

        public async Task<List<TaikhoanModel>> GetAll()
        {
            var Taikhoans = await _context.Taikhoans!.ToListAsync();
            return _mapper.Map<List<TaikhoanModel>>(Taikhoans);
        }

        public async Task<TaikhoanModel> GetByID(string id)
        {
            var Taikhoan = await _context.Taikhoans!.FindAsync(id);
            return _mapper.Map<TaikhoanModel>(Taikhoan);
        }

        public async Task Update(string id, TaikhoanModel Taikhoan)
        {
            if(id == Taikhoan.MaTk)
            {
                var updateTaikhoan = _mapper.Map<Taikhoan>(Taikhoan);
                _context.Taikhoans!.Update(updateTaikhoan);
                await _context.SaveChangesAsync();
            }    
        }

    }
}

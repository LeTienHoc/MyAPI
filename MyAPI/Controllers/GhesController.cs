﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models;
using MyAPI.Repositories;
using System.Data;
using System.Security.Claims;

namespace MyAPI.Controllers
{
    //[Authorize(Roles = "quantrinhakich")]
    [Route("api/[controller]")]
    [ApiController]
    public class GhesController : ControllerBase
    {
        private readonly IGheRepository _GheRepo;
        private readonly MyDbContext _context;

        public GhesController(IGheRepository repo,MyDbContext context)
        {
            _GheRepo = repo;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        [Route("GheCuaMoiNhakich")]
        public async Task<IActionResult> GetAllGheNK()
        {
            try
            {
                string role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value!;
                if(role.Equals("admin"))
                {
                    return Ok(await _GheRepo.GetAll());
                }    
                else if(role.Equals("Quản lý"))
                {
                    string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                    var mank = (from nk in _context.Nhakiches
                                where nk.MaNhaKich == idtaikhoan
                                select nk.MaNhaKich).SingleOrDefault()!.ToString();
                    var ghe = (from dv in _context.Ghes
                               where dv.MaNhaKich!.Equals("" + mank + "")
                               select new
                               {
                                   MaGhe = dv.MaGhe,
                                   MaNhaKich = dv.MaNhaKich,
                                   Hang = dv.Hang,
                                   Seat = dv.Seat,                                
                               }).ToList();
                    return Ok(ghe);
                }
                return Ok();
                
            }
            catch
            {
                return BadRequest();
            }
        }
        //[HttpGet]
        //public async Task< IActionResult> GetAllGhe()
        //{
        //    try
        //    {
        //        return Ok(await _GheRepo.GetAll());
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGheByID(string id)
        {
            var Ghe= await _GheRepo.GetByID(id);
            return  Ghe==null ?NotFound():Ok(Ghe);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddNewGhe(GheModel model)
        {
            try
            {
                string idtaikhoan = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                var mank = (from nk in _context.Nhakiches
                            where nk.MaNhaKich == idtaikhoan
                            select nk.MaNhaKich).SingleOrDefault()!.ToString();
                model.MaNhaKich = mank;
                var newGheId = await _GheRepo.Add(model);
                var Ghe = await _GheRepo.GetByID(newGheId);
                return Ghe == null ? NotFound() : Ok(Ghe);
            }
            catch
            {
                return BadRequest();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGhe(string id,GheModel model)
        {
            await _GheRepo.Update(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGhe([FromRoute] string id)
        {
            await _GheRepo.Delete(id);
            return Ok();
        }
    }
}
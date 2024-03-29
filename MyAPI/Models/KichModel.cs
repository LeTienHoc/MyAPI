﻿using MyAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAPI.Models
{
    public class KichModel
    {
      
        public string MaKich { get; set; }
        
        public string? MaNhaKich { get; set; }
     
        public string? TenKich { get; set; }
        
        public string? MoTa { get; set; }
       
        public string? Image { get; set; }
        
        public DateTime? NgayBd { get; set; }
        
        public DateTime? NgayKt { get; set; }
        
        public string? TheLoai { get; set; }
        
        public ulong? TrangThai { get; set; }
        [NotMapped]
        public string ImageFile { get; set; }
        //[NotMapped]
        //public string ImageSrc { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Kich
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
    }
}

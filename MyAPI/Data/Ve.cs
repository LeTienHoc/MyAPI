using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Ve
    {
        public string MaVe { get; set; }
        [Required]
        public string? MaXc { get; set; } 
        public string? MaKh { get; set; } 
        public string? MaTk { get; set; }
        [Required]
        public string? MaGhe { get; set; }
        [Required]
        public float? TongGia { get; set; }
        [Required]
        public DateTime? NgayDatVe { get; set; }
        [Required]
        public int TinhTrang { get; set; }
    }
}

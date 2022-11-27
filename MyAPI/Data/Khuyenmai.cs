using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Khuyenmai
    {
        [Required]
        public string MaKm { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        public string? ChuDe { get; set; }
        public string? NoiDung { get; set; }
        [Required]
        public DateTime? NgayBd { get; set; }
        [Required]
        public DateTime? NgayKt { get; set; }
        public string? Image { get; set; }
    }
}

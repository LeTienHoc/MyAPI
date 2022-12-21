using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Xuatchieu
    {
        public string MaXc { get; set; }
        [Required]
        public string? MaKich { get; set; }
        [Required]
        public string? MaLichChieu { get; set; }
        [Required]
        public DateTime? NgayGio { get; set; }
        public TimeSpan? Thoiluong { get; set; }

    }
}

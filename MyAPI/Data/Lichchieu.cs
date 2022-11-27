using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Lichchieu
    {
        public string MaLichChieu { get; set; }
        public string MaNhaKich { get; set; }
        [Required]
        public DateTime? NgayBd { get; set; }
        [Required]
        public DateTime? NgayKt { get; set; }
    }
}

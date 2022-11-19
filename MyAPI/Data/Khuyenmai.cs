using System;
using System.Collections.Generic;

namespace MyAPI.Data
{
    public partial class Khuyenmai
    {
        public string MaKm { get; set; }
        public string? MaKich { get; set; }
        public string? ChuDe { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayBd { get; set; }
        public DateTime? NgayKt { get; set; }
        public string? Image { get; set; }
    }
}

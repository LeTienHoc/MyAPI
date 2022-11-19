using System;
using System.Collections.Generic;

namespace MyAPI.Data
{
    public partial class Xuatchieu
    {
        public string MaXc { get; set; }
        public string? MaKich { get; set; }
        public string? MaLichChieu { get; set; }
        public DateTime? NgayGio { get; set; }
    }
}

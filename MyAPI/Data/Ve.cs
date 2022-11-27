using System;
using System.Collections.Generic;

namespace MyAPI.Data
{
    public partial class Ve
    {
        public string MaVe { get; set; }
        public string? MaXc { get; set; }
        public string? MaKh { get; set; }
        public string? MaTk { get; set; }
        public string? MaGhe { get; set; }
        public float? TongGia { get; set; }
        public DateTime? NgayDatVe { get; set; }
        public ulong? TinhTrang { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace MyAPI.Data
{
    public partial class Taikhoan
    {
        public string MaTk { get; set; }
        public string? TenTaiKhoan { get; set; }
        public string? MatKhau { get; set; }
        public string? ConfirmMatkhau { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }
        public string? LoaiTaiKhoan { get; set; }
    }
}

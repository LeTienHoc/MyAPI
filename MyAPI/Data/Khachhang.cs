using System;
using System.Collections.Generic;

namespace MyAPI.Data
{
    public partial class Khachhang
    {
        public string MaKh { get; set; }
        public string? TenKh { get; set; }
        public string? TenTaiKhoan { get; set; }
        public string? MatKhau { get; set; }
        public string? ConfirmMatKhau { get; set; }
        public string? DiaChi { get; set; }
        public string? Sdt { get; set; }
        public string? Email { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? Avarta { get; set; }
    }
}

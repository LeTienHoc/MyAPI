using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Khachhang
    {
        public string MaKh { get; set; }
        [Required]
        public string? TenKh { get; set; }
        [Required]
        public string? TenTaiKhoan { get; set; }
        [Required]
        public string? MatKhau { get; set; }
        [Required]
        public string? ConfirmMatKhau { get; set; }
        [Required]
        public string? DiaChi { get; set; }
        [Required]
        [MaxLength(10)]
        public string? Sdt { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public DateTime? NgaySinh { get; set; }
        public string? Avarta { get; set; }
    }
}

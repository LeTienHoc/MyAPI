using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Taikhoan
    {
        public string MaTk { get; set; }
        [Required]
        public string? TenTaiKhoan { get; set; }
        [Required]
        public string? MatKhau { get; set; }
        [Required]
        public string? ConfirmMatkhau { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Sdt { get; set; }
        [Required]
        public string? LoaiTaiKhoan { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class KhachhangModel
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
        public string? Email { get; set; }
        [Required]
        public DateTime? NgaySinh { get; set; }
    }
}

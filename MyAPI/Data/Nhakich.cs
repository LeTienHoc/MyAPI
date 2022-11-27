using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Nhakich
    {
        public string MaNhaKich { get; set; }
        [Required]
        public string? TenNhaKich { get; set; }
        [Required]
        [MaxLength(10)]
        public string? SoDienThoai { get; set; }
        [Required]
        public string? DiaChi { get; set; }
    }
}

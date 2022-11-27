using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Dienvien
    {
        [Required]
        public string MaDienVien { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        [Required]
        public string? TenDienVien { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Daodien
    {
        [Required]
        public string MaDaoDien { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        [Required]
        public string? TenDaoDien { get; set; }
    }
}

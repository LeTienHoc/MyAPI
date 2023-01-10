using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class KichDaodien
    {
        [Required]
        public string? MaKich { get; set; }
        [Required]
        public string? MaDaodien { get; set; }
    }
}

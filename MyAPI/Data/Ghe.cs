﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public partial class Ghe
    {
        public string MaGhe { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        [Required]
        public string? Hang { get; set; }
        [Required]
        public int? Seat { get; set; }

    }
}

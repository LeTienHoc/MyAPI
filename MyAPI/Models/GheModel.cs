using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class GheModel
    {
        public string MaGhe { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        [Required]
        public string? Hang { get; set; }
        [Required]
        public int? Seat { get; set; }
        public int? Status { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class SeatModel
    {

        public string MaGhe { get; set; }
        [Required]
        public string? NhaKich { get; set; }
        [Required]
        public string? Hang { get; set; }
        [Required]
        public int? Seat { get; set; }
        public int? Status { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class LichchieuModel
    {
        [Required]
        public string MaLichChieu { get; set; }
        [Required]
        public string MaNhaKich { get; set; }
        [Required]
        public DateTime? NgayBd { get; set; }
        [Required]
        public DateTime? NgayKt { get; set; }
    }
}

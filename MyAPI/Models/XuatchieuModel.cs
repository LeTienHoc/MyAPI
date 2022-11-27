using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class XuatchieuModel
    {
        public string MaXc { get; set; }
        [Required]
        public string? MaKich { get; set; }
        [Required]
        public string? MaLichChieu { get; set; }
        [Required]
        public DateTime? NgayGio { get; set; }
    }
}

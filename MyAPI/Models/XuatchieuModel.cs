using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

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
        public TimeSpan? Thoiluong { get; set; }
    }
}

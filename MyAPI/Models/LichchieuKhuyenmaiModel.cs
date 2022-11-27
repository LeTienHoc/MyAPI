using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class LichchieuKhuyenmaiModel
    {
        [Required]
        public string MaLichChieu { get; set; }
        [Required]
        public string MaKM { get; set; }
    }
}

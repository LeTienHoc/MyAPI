using System.ComponentModel.DataAnnotations;

namespace MyAPI.Data
{
    public class Lichchieu_Khuyenmai
    {
        [Required]
        public string MaLichChieu { get; set; }
        [Required]
        public string MaKM { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class VeModel
    {
        public string MaVe { get; set; }
        [Required]
        public string? MaXc { get; set; } 
        public string? MaKh { get; set; } 

        public string? MaTk { get; set; }
        [Required]
        public string? MaGhe { get; set; }
        [Required]
        public float? TongGia { get; set; }
        [Required]
        public DateTime? NgayDatVe { get; set; }
        [Required]
        public ulong? TinhTrang { get; set; }
    }
}

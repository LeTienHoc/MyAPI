using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class KichModel
    {
        [Required]
        public string MaKich { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        [Required]
        public string? TenKich { get; set; }
        [Required]
        public string? MoTa { get; set; }
        [Required]
        public string? DaoDien { get; set; }
        [Required]
        public string? Image { get; set; }
        [Required]
        public DateTime? NgayBd { get; set; }
        [Required]
        public DateTime? NgayKt { get; set; }
        [Required]
        public string? TheLoai { get; set; }
        [Required]
        public ulong? TrangThai { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class NhakichModel
    {
        public string MaNhaKich { get; set; }
        [Required]
        public string? TenNhaKich { get; set; }
        [Required]
        [MaxLength(10)]
        public string? SoDienThoai { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? DiaChi { get; set; }
    }
}

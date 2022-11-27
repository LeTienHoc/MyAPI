using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class DienvienModel
    {
        [Required]
        public string MaDienVien { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        [Required]
        public string? TenDienVien { get; set; }
    }
}

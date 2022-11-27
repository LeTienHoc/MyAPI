using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class KichDienvienModel
    {
        [Required]
        public string? MaKich { get; set; }
        [Required]
        public string? MaDienVien { get; set; }
    }
}

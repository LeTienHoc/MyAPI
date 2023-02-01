using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class DaodienModel
    {
        [Required]
        public string MaDaoDien { get; set; }
        [Required]
        public string? MaNhaKich { get; set; }
        [Required]
        public string? TenDaoDien { get; set; }

    }
}

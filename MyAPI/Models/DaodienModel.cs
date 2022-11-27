using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class DaodienModel
    {
        [Required]        
        
        public string MaDaoDien { get; set; }
        public string? MaNhaKich { get; set; }
        public string? TenDaoDien { get; set; }
    }
}

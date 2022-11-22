using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string? TenTaiKhoan { get; set; }
        [Required]
        [MaxLength (250)]
        public string? MatKhau { get; set; }
    }
}

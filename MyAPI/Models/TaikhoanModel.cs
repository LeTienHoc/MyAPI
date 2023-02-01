using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class TaikhoanModel
    {
        public string MaTk { get; set; }
        [Required]
        public string? TenTaiKhoan { get; set; }
        [Required]
        public string? MatKhau { get; set; }
        [Required]
        public string? ConfirmMatkhau { get; set; }
        [Required]
        public string? LoaiTaiKhoan { get; set; }
    }
}

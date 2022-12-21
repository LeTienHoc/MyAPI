using MyAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class KichPageModel
    {
        public string? TenKich { get; set; }

        public string? MoTa { get; set; }

        public string? DaoDien { get; set; }

        public string? Image { get; set; }

        public DateTime? NgayBd { get; set; }

        public DateTime? NgayKt { get; set; }

        public string? TheLoai { get; set; }
        public string DienVien { get; set; }

        //public List<string> DienViens { get; set; }
        
    }
}

using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class ResponseTypeSeat
    {
        public string Maghe { get; set; }
        public string? NhaKich { get; set; }
        
        public int? Seat { get; set; }
        private int seatStatus { get; set; }
        public ResponseTypeSeat(string maghe, string? nhaKich, int? seat, int seatStatus)
        {
            Maghe = maghe;
            NhaKich = nhaKich;
            Seat = seat;
            this.seatStatus = seatStatus;
        }
        public ResponseTypeSeat()
        {

        }
    }
}

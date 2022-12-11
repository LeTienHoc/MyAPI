using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class SeatBookingModel
    {
        public string? Hang { get; set; }
        public int? Seat { get; set; }
        public string? status { get; set; }
    }
}

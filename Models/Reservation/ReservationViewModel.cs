using Newtonsoft.Json;

namespace HotelBookingAppReact.Models.Reservation
{
    public class ReservationViewModel
    {
        public Guid id { get; set; }
        [JsonProperty("checkIn")]
        public DateTime checkIn { get; set; } = DateTime.Now;
        [JsonProperty("checkOut")]
        public DateTime checkOut { get; set; } = DateTime.Now;
        public string? userId { get; set; }
        [JsonProperty("roomId")]
        public int? roomId { get; set; }
        public double? totalPrice { get; set; }
    }
}

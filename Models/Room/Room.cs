
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelBookingAppReact.Models.Room
{
    public class Room
    {
        [Key]
        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }
        public IEnumerable<Models.Facility.Facility> Facilities { get; set; } = new List<Models.Facility.Facility>();
        public double PricePerNight { get; set; }
        [JsonIgnore]
        public IEnumerable<Reservation.Reservation> Reservations { get; set; } = new List<Reservation.Reservation>();
    }
}

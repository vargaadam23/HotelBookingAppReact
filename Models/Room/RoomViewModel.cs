using System.ComponentModel.DataAnnotations;

namespace HotelBookingAppReact.Models.Room
{
    public class RoomViewModel
    {
        public int RoomNumber { get; set; }
        [Range(0,5)]
        public int NumberOfBeds { get; set; }
        [Range(0, Double.MaxValue)]
        public double PricePerNight { get; set; }
        public ICollection<Guid> Facilities { get; set; } = new List<Guid>();
    }
}

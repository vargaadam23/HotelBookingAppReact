namespace HotelBookingAppReact.Models.Room
{
    public class RoomViewModel
    {
        public int RoomNumber { get; set; }
        public int NumberOfBeds { get; set; }
        public double PricePerNight { get; set; }
        public ICollection<Guid> Facilities { get; set; } = new List<Guid>();
    }
}

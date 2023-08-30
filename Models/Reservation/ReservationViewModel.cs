namespace HotelBookingAppReact.Models.Reservation
{
    public class ReservationViewModel
    {
        public DateTime checkIn { get; set; } = DateTime.Now;
        public DateTime checkOut { get; set; } = DateTime.Now;
        public string? userId { get; set; }
        public int? roomId { get; set; }
    }
}

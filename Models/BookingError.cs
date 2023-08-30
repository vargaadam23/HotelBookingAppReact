namespace HotelBookingAppReact.Models
{
    public class BookingError
    {
        public BookingError(Exception exception) {
            this.Message = exception.Message;
            ThrownAt = DateTime.Now;
        }

        public BookingError(string message)
        {
            this.Message = message;
            ThrownAt = DateTime.Now;
        }

        public DateTime ThrownAt { get; set; }

        public string Message { get; set; }
    }
}

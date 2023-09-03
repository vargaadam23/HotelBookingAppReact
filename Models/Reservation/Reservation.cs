using System.ComponentModel.DataAnnotations;

namespace HotelBookingAppReact.Models.Reservation
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; } = null;
        public double TotalPrice { get; set; }
        public Room.Room? Room { get; set; }
        public DateTime CheckIn { get; set; } = DateTime.Now;
        public DateTime CheckOut { get; set; } = DateTime.Now;
    }
}

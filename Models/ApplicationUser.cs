using Microsoft.AspNetCore.Identity;

namespace HotelBookingAppReact.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Models.Reservation.Reservation>? Reservations { get; set; } = new List<Models.Reservation.Reservation>();
    }
}
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace HotelBookingAppReact.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public ICollection<Models.Reservation.Reservation>? Reservations { get; set; } = new List<Models.Reservation.Reservation>();
    }
}
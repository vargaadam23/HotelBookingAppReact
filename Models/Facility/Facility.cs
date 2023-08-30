using System.ComponentModel.DataAnnotations;
using HotelBookingAppReact.Models.Room;
using Newtonsoft.Json;

namespace HotelBookingAppReact.Models.Facility
{
    public class Facility
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public IEnumerable<Room.Room> Rooms { get; set; } = new List<Room.Room>();
    }
}

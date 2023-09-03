using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
        [System.Text.Json.Serialization.JsonIgnore]
        public IEnumerable<Room.Room> rooms { get; set; } = new List<Room.Room>();
    }
}

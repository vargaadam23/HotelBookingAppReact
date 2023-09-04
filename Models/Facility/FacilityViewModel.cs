using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAppReact.Models.Facility
{
    public class FacilityViewModel
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("name")]
        [StringLength(50)]
        public string? Name { get; set; }
        [JsonProperty("description")]
        [StringLength(200)]
        public string? Description { get; set; }
    }
}

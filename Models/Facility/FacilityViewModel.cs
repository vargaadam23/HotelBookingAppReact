using Newtonsoft.Json;

namespace HotelBookingAppReact.Models.Facility
{
    public class FacilityViewModel
    {
        [JsonProperty("id")]
        public Guid? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}

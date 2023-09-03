namespace HotelBookingAppReact.Models
{
    public class SuccessResponse
    {
        public SuccessResponse(string message) 
        {
            this.message = message;
        }

        public SuccessResponse()
        {
         
        }

        public string message { get; set; } = "Created successfully!";
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}

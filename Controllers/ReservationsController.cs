using HotelBookingAppReact.Models;
using HotelBookingAppReact.Models.Reservation;
using HotelBookingAppReact.Services.FacilityService;
using HotelBookingAppReact.Services.ReservationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAppReact.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService reservationService;
        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return reservationService.HasRecords() ? reservationService.GetReservationsForUser() : new List<Reservation>();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IEnumerable<Reservation> GetAll()
        {
            return reservationService.HasRecords() ? reservationService.GetAllReservations() : new List<Reservation>();
        }

        [HttpDelete]
        public ActionResult Cancel([FromBody] Guid guid)
        {
            if (guid == null)
            {
                return NotFound();
            }

            try
            {
                var isReservationDeleted = reservationService.CacelReservation(guid);
                return isReservationDeleted ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }
    }
}

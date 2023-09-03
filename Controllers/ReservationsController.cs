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
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                return Ok(reservationService.HasRecords() ? reservationService.GetReservationsForUser() : new List<Reservation>());
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(reservationService.HasRecords() ? reservationService.GetAllReservations() : new List<Reservation>());
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpDelete]
        public IActionResult Cancel([FromBody]ReservationViewModel reservationViewModel)
        {
            if (reservationViewModel.id == null)
            {
                return NotFound(new BookingError("Reservation id not found"));
            }

            try
            {
                var isReservationDeleted = reservationService.CacelReservation((Guid)reservationViewModel.id);
                return isReservationDeleted ? Ok(new SuccessResponse("Reservation canceled!")) : BadRequest(new BookingError("Reservation could not be canceled"));
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }
    }
}

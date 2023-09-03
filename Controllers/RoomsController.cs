using HotelBookingAppReact.Models.Reservation;
using HotelBookingAppReact.Models.Room;
using HotelBookingAppReact.Services.ReservationService;
using HotelBookingAppReact.Services.RoomService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAppReact.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelBookingAppReact.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly IReservationService reservationService;

        public RoomsController(IRoomService roomService, IReservationService reservationService)
        {
            this.roomService = roomService;
            this.reservationService = reservationService;
        }

        [HttpGet]
        public IEnumerable<Room> List()
        {
            return !roomService.HasRecords() ? new List<Room>() : roomService.GetRooms();
        }

        [HttpGet]
        public ActionResult Get([FromQuery] int roomId)
        {
            if(roomId == null)
            {
                return NotFound();
            }

            return Ok(roomService.GetRoomById(roomId));
        }

        [HttpPost]
        public ActionResult Reserve([FromBody] ReservationViewModel reservationViewModel)
        {
            if (reservationViewModel.roomId == null)
            {
                return NotFound();
            }
            try
            {
                bool isReserved = reservationService.Reserve(reservationViewModel);

                return isReserved ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([FromBody] RoomViewModel roomViewModel)
        {
            try
            {
                var isRoomCreated = roomService.CreateRoom(roomViewModel);
                return isRoomCreated ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromBody] int roomId)
        {
            if(roomId == null)
            {
                return NotFound();
            }

            try
            {
                var isRoomDeleted = roomService.DeleteRoom(roomId);
                return isRoomDeleted ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        //[HttpPut]
        //[Authorize(Roles = "Administrator")]
        //public ActionResult Delete([FromBody] RoomViewModel roomViewModel)
        //{

        //    try
        //    {
        //        var isRoomDeleted = roomService.DeleteRoom(roomId);
        //        return isRoomDeleted ? Ok() : BadRequest();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new BookingError(ex));
        //    }
        //}


    }
}

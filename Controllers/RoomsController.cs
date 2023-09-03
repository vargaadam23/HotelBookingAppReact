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
        public IActionResult List()
        {
            try
            {
                return Ok(!roomService.HasRecords() ? new List<Room>() : roomService.GetRooms());
            }catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
            
        }

        [HttpGet]
        public IActionResult Get([FromBody] RoomViewModel roomViewModel )
        {
            if(roomViewModel.RoomNumber == null)
            {
                return NotFound(new BookingError("Room id not provided!"));
            }

            try
            {
                return Ok(roomService.GetRoomById(roomViewModel.RoomNumber));
            }catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPost]
        public IActionResult Reserve([FromBody] ReservationViewModel reservationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (reservationViewModel.roomId == null)
            {
                return NotFound(new BookingError("No room id provided for reservation"));
            }

            try
            {
                bool isReserved = reservationService.Reserve(reservationViewModel);

                return isReserved ? Ok(new SuccessResponse("Room reserved successfully!")) : BadRequest(new BookingError("Reservation failed!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] RoomViewModel roomViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var isRoomCreated = roomService.CreateRoom(roomViewModel);
                return isRoomCreated ? Ok(new SuccessResponse("Room created successfully!")) : BadRequest(new BookingError("Room creation failed!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] RoomViewModel roomViewModel)
        {
            if(roomViewModel.RoomNumber == null)
            {
                return NotFound(new BookingError("Room id was not provided"));
            }

            try
            {
                var isRoomDeleted = roomService.DeleteRoom(roomViewModel.RoomNumber);
                return isRoomDeleted ? Ok(new SuccessResponse("Room deleted successfully!")) : BadRequest(new BookingError("Room deletion failed!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] RoomViewModel roomViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (roomViewModel.RoomNumber == null)
            {
                return NotFound(new BookingError("Room id was not provided"));
            }

            try
            {
                var isRoomDeleted = roomService.UpdateRoom(roomViewModel);
                return isRoomDeleted ? Ok(new SuccessResponse("Room updated successfully!")) : BadRequest(new BookingError("Room update failed!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }


    }
}

using HotelBookingAppReact.Models;
using HotelBookingAppReact.Models.Facility;
using HotelBookingAppReact.Models.Room;
using HotelBookingAppReact.Services.FacilityService;
using HotelBookingAppReact.Services.RoomService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace HotelBookingAppReact.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly IFacilityService facilityService;

        public FacilitiesController(IFacilityService facilityService)
        {
            this.facilityService = facilityService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(facilityService.HasRecords() ?facilityService.GetFacilities() : new List<Facility>());
        }

        [HttpPost]
        public ActionResult Create([FromBody] FacilityViewModel facilityViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var isFacilityCreated = facilityService.CreateFacility(facilityViewModel);
                return isFacilityCreated ?
                    Ok(new SuccessResponse("Facility successfully created!")) :
                    BadRequest(new BookingError("Could not create facility!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] FacilityViewModel facilityViewModel)
        {
            if (facilityViewModel.Id == null)
            {
                return NotFound(new BookingError("No id provided!"));
            }

            try
            {
                var isFacilityDeleted = facilityService.DeleteFacility((Guid)facilityViewModel.Id);
                return isFacilityDeleted? 
                    Ok(new SuccessResponse("Facility successfully deleted!")) :
                    BadRequest(new BookingError("Could not delete facility!"));
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }
    }
}

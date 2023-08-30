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
        public IEnumerable<Facility> List()
        {
            return facilityService.HasRecords() ? facilityService.GetFacilities() : new List<Facility>();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create([FromBody] FacilityViewModel facilityViewModel)
        {
            Debug.Print(facilityViewModel.Name);
            try
            {
                var isFacilityCreated = facilityService.CreateFacility(facilityViewModel);
                return isFacilityCreated ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete([FromBody] Guid guid)
        {
            if (guid == null)
            {
                return NotFound();
            }

            try
            {
                var isFacilityDeleted = facilityService.DeleteFacility(guid);
                return isFacilityDeleted? Ok() : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new BookingError(ex));
            }
        }
    }
}

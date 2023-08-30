using HotelBookingAppReact.Models.Facility;

namespace HotelBookingAppReact.Services.FacilityService
{
    public interface IFacilityService
    {
        public IEnumerable<Facility> GetFacilities();
        public bool CreateFacility(FacilityViewModel facilityViewModel);
        public bool DeleteFacility(Guid id);
        public bool HasRecords();
        public Facility GetFacilityById(Guid id);
        public IEnumerable<Facility> GetFacilitiesFromIds(IEnumerable<Guid> ids);

    }
}

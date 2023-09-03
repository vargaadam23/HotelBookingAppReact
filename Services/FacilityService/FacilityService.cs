using HotelBookingAppReact.Models.Facility;
using HotelBookingAppReact.Repositories.Facility;

namespace HotelBookingAppReact.Services.FacilityService
{
    public class FacilityService : IFacilityService
    {
        private readonly IFacilityRepository facilityRepository;

        private readonly ILogger<FacilityService> logger;

        public FacilityService(IFacilityRepository facilityRepository, ILogger<FacilityService> logger)
        {
            this.facilityRepository = facilityRepository;
            this.logger = logger;
        }

        public bool CreateFacility(FacilityViewModel facilityViewModel)
        {
            Facility facility = GetFacilityFromViewModel(facilityViewModel);

            facilityRepository.Create(facility);
            return true;
        }

        public bool DeleteFacility(Guid id)
        {
            var facility = GetFacilityById(id);

            if (facility == null)
            {
                throw new Exception("Facility with id " + id.ToString() + " not found!");
            }

            facilityRepository.Delete(facility);
            return true;
        }

        public IEnumerable<Facility> GetFacilities()
        {
            return facilityRepository.List().Result;
        }

        public IEnumerable<Facility> GetFacilitiesFromIds(IEnumerable<Guid> ids)
        {
            return facilityRepository.ListWhere(e => ids.Contains(e.Id));
        }

        public Facility? GetFacilityById(Guid id)
        {
            return facilityRepository.Get(id).Result;
        }

        public bool HasRecords()
        {
            return facilityRepository.HasRecords();
        }

        private Facility GetFacilityFromViewModel(FacilityViewModel facilityViewModel)
        {
            Facility facility = new Facility();
            facility.Name = facilityViewModel.Name;
            facility.Description = facilityViewModel.Description;

            return facility;
        }

    }
}

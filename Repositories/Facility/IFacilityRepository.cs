namespace HotelBookingAppReact.Repositories.Facility
{
    public interface IFacilityRepository
    {
        public Task<IEnumerable<Models.Facility.Facility>> List();
        public void Create(Models.Facility.Facility facility);
        public void Delete(Models.Facility.Facility facility);
        public bool HasRecords();
        public Task<Models.Facility.Facility> Get(Guid id);
        public IEnumerable<Models.Facility.Facility> ListWhere(Func<Models.Facility.Facility, bool> func);
    }
}

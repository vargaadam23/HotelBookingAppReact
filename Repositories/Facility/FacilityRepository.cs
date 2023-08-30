using HotelBookingAppReact.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAppReact.Repositories.Facility
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly ApplicationDbContext _context;
        public FacilityRepository(ApplicationDbContext context) 
        { 
            this._context = context;
        }
        public void Create(Models.Facility.Facility facility)
        {
            _context.Facilities.Add(facility);
            _context.SaveChanges();
        }

        public void Delete(Models.Facility.Facility facility)
        {
            _context.Facilities.Remove(facility);
            _context.SaveChanges();
        }

        public bool HasRecords()
        {
            return _context.Facilities!=null;
        }

        public async Task<IEnumerable<Models.Facility.Facility>> List()
        {
            return await _context.Facilities?.ToListAsync();
        }

        public async Task<Models.Facility.Facility> Get(Guid id)
        {
            return await _context.Facilities.FindAsync(id);
        }

        public IEnumerable<Models.Facility.Facility> ListWhere(Func<Models.Facility.Facility, bool> func)
        {
            return _context.Facilities.Where(func).ToList();
        }
    }
}

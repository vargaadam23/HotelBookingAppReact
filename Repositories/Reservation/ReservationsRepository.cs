using HotelBookingAppReact.Data;
using HotelBookingAppReact.Models.Facility;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAppReact.Repositories.Reservation
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly ApplicationDbContext _context;
        public ReservationsRepository(ApplicationDbContext context) {  _context = context; }
        public void Create(Models.Reservation.Reservation reservation)
        {
            _context.Reservations?.Add(reservation);
            _context.SaveChanges();
        }

        public void Delete(Models.Reservation.Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public async Task<Models.Reservation.Reservation?> Get(Guid guid)
        {
            return await _context.Reservations.Include(e=>e.Room).Include(e=>e.User).FirstOrDefaultAsync(e => e.Id == guid);
        }

        public bool HasRecords()
        {
            return _context.Reservations != null;
        }

        public async Task<IEnumerable<Models.Reservation.Reservation>?> List()
        {
            return await _context.Reservations?.Include(e => e.Room).Include(e => e.User).ToListAsync();
        }

        public IEnumerable<Models.Reservation.Reservation> ListWhere(Func<Models.Reservation.Reservation, bool> wherePredicate)
        {
            return _context.Reservations.Include(e => e.Room).Include(e => e.User).Where(wherePredicate).ToList();
        }

        public void Update(Models.Reservation.Reservation reservation)
        {
            _context.Reservations?.Attach(reservation);
            var entry = _context.Entry(reservation);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}

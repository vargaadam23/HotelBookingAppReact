using HotelBookingAppReact.Controllers;
using HotelBookingAppReact.Data;
using HotelBookingAppReact.Models.Facility;
using HotelBookingAppReact.Models.Reservation;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAppReact.Repositories.Room
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly ApplicationDbContext context;

        public RoomsRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Models.Room.Room room)
        {
            context.Rooms.Add(room);
            context.SaveChanges();
        }

        public void Delete(Models.Room.Room room)
        {
            context.Rooms.Remove(room);
            context.SaveChanges();
        }

        public async Task<Models.Room.Room?> Get(int id)
        {
            return await context.Rooms.Include(e=>e.Facilities).FirstOrDefaultAsync(x => x.RoomNumber == id);
        }

        public bool HasRecords()
        {
            return context.Rooms != null;
        }

        public async Task<IEnumerable<Models.Room.Room>?> List()
        {
            return await context.Rooms?.Include(e => e.Facilities)?.ToListAsync();
        }

        public void Update(Models.Room.Room room)
        {
            context.Rooms?.Attach(room);
            var entry = context.Entry(room);
            entry.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}

namespace HotelBookingAppReact.Repositories.Room
{
    public interface IRoomsRepository
    {
        public Task<IEnumerable<Models.Room.Room>> List();
        public void Create(Models.Room.Room room);
        public Task<Models.Room.Room> Get(int id);
        public bool HasRecords();
        public void Delete(Models.Room.Room room);
        public void Update(Models.Room.Room room);

    }
}

namespace HotelBookingAppReact.Repositories.Reservation
{
    public interface IReservationsRepository
    {
        public void Create(Models.Reservation.Reservation reservation);
        public Task<IEnumerable<Models.Reservation.Reservation>> List();
        public Task<Models.Reservation.Reservation> Get(Guid guid);
        public bool HasRecords();
        public void Delete(Models.Reservation.Reservation reservation);
        public void Update(Models.Reservation.Reservation reservation);
        public IEnumerable<Models.Reservation.Reservation> ListWhere(Func<Models.Reservation.Reservation, bool> wherePredicate);
    }
}

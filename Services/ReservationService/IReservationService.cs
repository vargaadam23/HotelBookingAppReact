using HotelBookingAppReact.Models.Reservation;
using HotelBookingAppReact.Models.Room;

namespace HotelBookingAppReact.Services.ReservationService
{
    public interface IReservationService
    {
        public bool Reserve(ReservationViewModel reservationViewModel);
        public bool UpdateReservation(ReservationViewModel reservationViewModel);
        public bool CacelReservation(Guid reservationId);
        public IEnumerable<Models.Reservation.Reservation> GetAllReservations();
        public bool HasRecords();
        public Models.Reservation.Reservation GetByReservationId(Guid? reservationId);
        public bool ValidateReservation(DateTime checkIn, DateTime checkOut, int? RoomNumber);
        public IEnumerable<Models.Reservation.Reservation> GetReservationsForUser();
        public Double CalculatePrice(Reservation reservation);
    }
}

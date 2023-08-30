using HotelBookingAppReact.Models.Reservation;
using HotelBookingAppReact.Models.Room;
using HotelBookingAppReact.Repositories.Reservation;
using HotelBookingAppReact.Services.RoomService;
using System.Security.Claims;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingAppReact.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly int MAX_DAYS_TO_EDIT = 3;

        private readonly IReservationsRepository reservationsRepository;
        private readonly ILogger<ReservationService> logger;
        private readonly IRoomService roomService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ReservationService(ILogger<ReservationService> logger, IReservationsRepository reservationsRepository, IRoomService roomService, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.reservationsRepository = reservationsRepository;
            this.roomService = roomService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool CacelReservation(Guid reservationId)
        {
            var user = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                throw new Exception("User has to be logged in");
            }

            try
            {
                var reservation = GetByReservationId(reservationId);

                if (reservation == null)
                {
                    return false;
                }

                if(reservation.UserId != user)
                {
                    return false;
                }

                reservationsRepository.Delete(reservation);

                return true;
            }catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return reservationsRepository.List().Result;
        }

        public Reservation GetByReservationId(Guid? reservationId)
        {
            if(reservationId == null)
            {
                return null;
            }

            return reservationsRepository.Get(reservationId.Value).Result;
        }

        public bool HasRecords()
        {
            return reservationsRepository.HasRecords();
        }

        public bool Reserve(ReservationViewModel reservationViewModel)
        {
            bool isReservationValid = ValidateReservation(reservationViewModel.checkIn, reservationViewModel.checkOut, reservationViewModel.roomId);

            if (isReservationValid)
            {
                Reservation reservation = CreateReservationFromViewModel(reservationViewModel);
                reservationsRepository.Create(reservation);

                return true;
            }

            return false;
        }

        public bool UpdateReservation(ReservationViewModel reservationViewModel)
        {
            throw new NotImplementedException();
        }

        public bool ValidateReservation(DateTime checkIn, DateTime checkOut, int? RoomNumber)
        {
            if((checkOut - checkIn).TotalDays < 1)
            {
                throw new ValidationException("Invalid dates, please make sure check in date is before check out date with at least 1 day difference!");
            }

            var activeReservations = reservationsRepository.ListWhere((Reservation reservation) => {
                return reservation.Room.RoomNumber == RoomNumber && (IsDateBetweenPair(checkIn, reservation) || IsDateBetweenPair(checkOut, reservation));
            });

            if(activeReservations.Any())
            {
                throw new ValidationException("The room is reserved for the picked dates, please try another room or change the reservation dates!"+activeReservations.First().ToString());
            }

            return true;
        }

        private bool IsDateBetweenPair(DateTime dateToCompare,Reservation reservation)
        {
            return DateTime.Compare(dateToCompare, reservation.CheckIn) >= 0 && DateTime.Compare(dateToCompare, reservation.CheckOut) < 0;
        }

        private Reservation CreateReservationFromViewModel(ReservationViewModel reservationViewModel)
        {
            var room = roomService.GetRoomById(reservationViewModel.roomId.Value);

            if (room == null)
            {
                throw new Exception("Reservation room missing");
            }

            var user = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                throw new Exception("User has to be logged in");
            }

            Reservation reservation = new Reservation
            {
                Room = room,
                UserId = user,
                CheckIn = reservationViewModel.checkIn,
                CheckOut = reservationViewModel.checkOut,
                TotalPrice = room.PricePerNight
            };

            return reservation;

        }

        public IEnumerable<Reservation> GetReservationsForUser()
        {
            var user = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return reservationsRepository.ListWhere((Reservation reservation) =>
            {
                return reservation.UserId == user;
            });
        }
    }
}

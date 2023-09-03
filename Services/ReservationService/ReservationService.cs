using HotelBookingAppReact.Models.Reservation;
using HotelBookingAppReact.Models.Room;
using HotelBookingAppReact.Repositories.Reservation;
using HotelBookingAppReact.Services.RoomService;
using System.Security.Claims;
using System.Security.Principal;
using System.ComponentModel.DataAnnotations;
using HotelBookingAppReact.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelBookingAppReact.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly int MAX_DAYS_TO_EDIT = 3;

        private readonly IReservationsRepository reservationsRepository;
        private readonly ILogger<ReservationService> logger;
        private readonly IRoomService roomService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        public ReservationService(
            ILogger<ReservationService> logger,
            IReservationsRepository reservationsRepository,
            IRoomService roomService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.reservationsRepository = reservationsRepository;
            this.roomService = roomService;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
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
                Reservation reservation = CreateReservationFromViewModel(reservationViewModel).Result;
                reservationsRepository.Create(reservation);
                System.Diagnostics.Debug.WriteLine(reservation.Id.ToString() + reservation.Room.RoomNumber.ToString() + reservation.TotalPrice.ToString() + reservation.CheckIn.ToString() + reservation.CheckOut.ToString());

                return true;
            }

            return false;
        }

        public bool UpdateReservation(ReservationViewModel reservationViewModel)
        {
            bool maxDaysExceeded = (reservationViewModel.checkIn - DateTime.Now).Days < MAX_DAYS_TO_EDIT;
            
            if (maxDaysExceeded)
            {
                return false;
            }

            if(reservationViewModel.id == null)
            {
                return false;
            }

            var reservation = reservationsRepository.Get(reservationViewModel.id).Result;

            if(reservation == null)
            {
                return false;
            }

            reservation.CheckIn = reservationViewModel.checkIn;
            reservation.CheckOut = reservationViewModel.checkOut;
            reservation.TotalPrice = CalculatePrice(reservation);

            reservationsRepository.Update(reservation);

            return true;
        }

        public bool ValidateReservation(DateTime checkIn, DateTime checkOut, int? RoomNumber)
        {
            if((checkOut - checkIn).TotalDays < 1)
            {
                throw new ValidationException("Invalid dates, please make sure check in date is before check out date with at least 1 day difference!");
            }

            var activeReservations = reservationsRepository.ListWhere((Reservation reservation) => {
                return
                    reservation.Room.RoomNumber == RoomNumber &&
                    (IsDateBetweenPair(checkIn, reservation) || IsDateBetweenPair(checkOut, reservation) ||
                    (DateTime.Compare(checkIn, reservation.CheckIn) < 0 && DateTime.Compare(checkOut, reservation.CheckOut) > 0));
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

        private async Task<Reservation> CreateReservationFromViewModel(ReservationViewModel reservationViewModel)
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
                User = await userManager.FindByIdAsync(user),
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

        public double CalculatePrice(Reservation reservation)
        {
            var timespan = reservation.CheckOut - reservation.CheckIn;
            return reservation.Room.PricePerNight * timespan.Days;
        }
    }
}

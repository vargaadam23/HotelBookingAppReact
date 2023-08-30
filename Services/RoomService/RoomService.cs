using AutoMapper.Internal;
using HotelBookingAppReact.Models.Room;
using HotelBookingAppReact.Repositories.Room;
using HotelBookingAppReact.Services.FacilityService;

namespace HotelBookingAppReact.Services.RoomService
{
    public class RoomService : IRoomService
    {
        private readonly Repositories.Room.IRoomsRepository roomRepository;
        private readonly IFacilityService facilityService;

        private readonly ILogger<RoomService> _logger;

        public RoomService(Repositories.Room.IRoomsRepository roomsRepository, ILogger<RoomService> logger, IFacilityService facilityService)
        {
            this.roomRepository = roomsRepository;
            this._logger = logger;
            this.facilityService = facilityService;
        }
        public bool CreateRoom(RoomViewModel roomViewModel)
        {
            try
            {
                Room room = CreateRoomFromViewModel(roomViewModel);

                roomRepository.Create(room);

                return true;
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool DeleteRoom(int roomId)
        {
            try
            {
                var room = GetRoomById(roomId);

                if (room != null)
                {
                    roomRepository.Delete(room);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public Room? GetRoomById(int roomId)
        {
            return roomRepository.Get(roomId).Result;
        }

        public IEnumerable<Room> GetRooms()
        {
            return roomRepository.List().Result;
        }

        public bool HasRecords()
        {
            return roomRepository.HasRecords();
        }

        public bool UpdateRoom(RoomViewModel roomViewModel)
        {
            try
            {
                if (roomViewModel == null || !HasRecords())
                {
                    return false;
                }

                var roomEntity = GetRoomById(roomViewModel.RoomNumber);

                if (roomEntity == null)
                {
                    return false;
                }

                UpdateRoomFromViewModel(roomEntity, roomViewModel);

                roomRepository.Update(roomEntity);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        private Room CreateRoomFromViewModel(RoomViewModel roomViewModel)
        {
            Room room = new Room();
            room.PricePerNight = roomViewModel.PricePerNight;
            room.NumberOfBeds = roomViewModel.NumberOfBeds;
            room.RoomNumber = roomViewModel.RoomNumber;

            var facilities = facilityService.GetFacilitiesFromIds(roomViewModel.Facilities);

            if (facilities.Any())
            {
                room.Facilities = facilities;
            }

            return room;
        }

        private void UpdateRoomFromViewModel(Room roomEntity, RoomViewModel roomViewModel) 
        {
            roomEntity.NumberOfBeds = roomViewModel.NumberOfBeds;
            roomEntity.PricePerNight = roomViewModel.PricePerNight;
            var facilities = facilityService.GetFacilitiesFromIds(roomViewModel.Facilities);

            if (facilities.Any())
            {
                roomEntity.Facilities = facilities;
            }
        }
    }
}

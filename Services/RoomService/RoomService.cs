using AutoMapper.Internal;
using HotelBookingAppReact.Models.Room;
using HotelBookingAppReact.Repositories.Room;
using HotelBookingAppReact.Services.FacilityService;
using Microsoft.AspNetCore.Identity;

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
            Room room = CreateRoomFromViewModel(roomViewModel);

            roomRepository.Create(room);

            return true;
        }

        public bool DeleteRoom(int roomId)
        {
            var room = GetRoomById(roomId);

            if (room != null)
            {
                roomRepository.Delete(room);
                return true;
            }

            throw new Exception("Room not found!");
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
            if (roomViewModel == null || !HasRecords())
            {
                throw new Exception("Room could not be updated!");
            }

            var roomEntity = GetRoomById(roomViewModel.RoomNumber);

            if (roomEntity == null)
            {
                throw new Exception("Room not found!");
            }

            UpdateRoomFromViewModel(roomEntity, roomViewModel);

            roomRepository.Update(roomEntity);

            return true;
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
            System.Diagnostics.Debug.WriteLine(room.Facilities.First().Name);


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

using HotelBookingAppReact.Models.Room;

namespace HotelBookingAppReact.Services.RoomService
{
    public interface IRoomService
    {
        public IEnumerable<Room> GetRooms();
        public bool CreateRoom(RoomViewModel roomViewModel);
        public bool DeleteRoom(int roomId);
        public bool UpdateRoom(RoomViewModel roomViewModel);
        public Models.Room.Room? GetRoomById(int roomId);
        public bool HasRecords();
    }
}

using HotelBookingAppReact.Models.Room;
using HotelBookingAppReact.Repositories.Room;
using HotelBookingAppReact.Services.FacilityService;
using HotelBookingAppReact.Services.RoomService;
using Xunit;
using Moq;


namespace HotelBookingAppReact.Tests
{
    public class RoomUnitTests
    {
        [Fact]
        public void CreateRoom_ValidRoomViewModel_ReturnsTrue()
        {
            // Arrange
            var mockRoomRepository = new Mock<IRoomsRepository>();
            var mockFacilityService = new Mock<IFacilityService>();
            var mockLogger = new Mock<ILogger<RoomService>>();
            var roomService = new RoomService(mockRoomRepository.Object, mockLogger.Object, mockFacilityService.Object);
            var roomViewModel = new RoomViewModel
            {
                NumberOfBeds = 1,
                PricePerNight = 22,
                Facilities = new List<Guid>()
            };

            // Act
            var result = roomService.CreateRoom(roomViewModel);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CreateRoom_InvalidRoomViewModel_ThrowsException()
        {
            // Arrange
            var mockRoomRepository = new Mock<IRoomsRepository>();
            var mockFacilityService = new Mock<IFacilityService>();
            var mockLogger = new Mock<ILogger<RoomService>>();
            var roomService = new RoomService(mockRoomRepository.Object, mockLogger.Object, mockFacilityService.Object);
            RoomViewModel roomViewModel = null; // Invalid data

            // Act & Assert
            Assert.Throws<Exception>(() => roomService.CreateRoom(roomViewModel));
        }

       
    }
}

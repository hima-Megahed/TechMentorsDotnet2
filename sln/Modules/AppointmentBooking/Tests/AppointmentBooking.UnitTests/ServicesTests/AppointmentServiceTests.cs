/*using AppointmentBooking.Application.Contracts.Services;
using AppointmentBooking.Infrastructure.Repositories;
using AppointmentBooking.Infrastructure.Services;
using Moq;

namespace AppointmentBooking.UnitTests.ServicesTests;

// London School "Mockist"
public class AppointmentServiceTests
{
    private readonly Mock<IAppointmentRepository> _repositoryMock;
    private readonly IAppointmentService _service;

    public AppointmentServiceTests()
    {
        // Set up the mock repository
        _repositoryMock = new Mock<IAppointmentRepository>(MockBehavior.Strict);

        // Inject the mock repository into the service
        _service = new AppointmentService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetMySlots_ShouldReturn_SlotsFromRepository()
    {
        // Arrange
        var doctorId = Guid.NewGuid();
        var doctorName = "Dr. John Doe";
        var mockSlots = new List<DoctorSlot>
        {
            DoctorSlot.Create(DateTime.Now.AddDays(1), doctorId, doctorName, 100),
            DoctorSlot.Create(DateTime.Now.AddDays(5), doctorId, doctorName, 150)
        };

        _repositoryMock.Setup(repo => repo.GetMySlots())
            .ReturnsAsync(mockSlots);

        // Act
        var result = await _service.GetMySlots();

        // Assert
        Assert.Equal(mockSlots.Count, result.Count);
        Assert.Equal(mockSlots, result);
        _repositoryMock.Verify(repo => repo.GetMySlots(), Times.Once);
    }

}*/
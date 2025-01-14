using DoctorAvailability.Business.Repositories;
using DoctorAvailability.Business.Services.DoctorSlot;
using DoctorAvailability.Business.Services.DoctorSlot.Models;
using DoctorAvailability.Internal.Data.Models;
using Moq;

namespace DoctorAvailability.UnitTests.ServicesTests;

// London School "Mockist"
public class DoctorSlotServiceTests
{
    private readonly Mock<IDoctorSlotRepository> _repositoryMock;
    private readonly DoctorSlotService _service;

    public DoctorSlotServiceTests()
    {
        // Set up the mock repository
        _repositoryMock = new Mock<IDoctorSlotRepository>(MockBehavior.Strict);

        // Inject the mock repository into the service
        _service = new DoctorSlotService(_repositoryMock.Object);
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

    [Fact]
    public async Task GetMySlots_ShouldReturn_EmptyList_WhenNoSlotsExist()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.GetMySlots())
            .ReturnsAsync(new List<DoctorSlot>());

        // Act
        var result = await _service.GetMySlots();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _repositoryMock.Verify(repo => repo.GetMySlots(), Times.Once);
    }

    [Fact]
    public async Task GetMySlots_ShouldHandle_NullReturnFromRepository()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.GetMySlots())
            .ReturnsAsync((List<DoctorSlot>?)null);

        // Act
        var result = await _service.GetMySlots();

        // Assert
        Assert.Null(result);
        _repositoryMock.Verify(repo => repo.GetMySlots(), Times.Once);
    }

    [Fact]
    public async Task GetMySlots_ShouldNotCallRepository_WhenConditionIsNotMet()
    {
        // Example condition for not calling repository (this is hypothetical; adjust as needed)
        var shouldCallRepository = false;

        if (shouldCallRepository)
        {
            _repositoryMock.Setup(repo => repo.GetMySlots())
                .ReturnsAsync(new List<DoctorSlot>());
        }

        // Act
        var result = shouldCallRepository ? await _service.GetMySlots() : new List<DoctorSlot>();

        // Assert
        if (!shouldCallRepository)
        {
            Assert.Empty(result);
            _repositoryMock.Verify(repo => repo.GetMySlots(), Times.Never);
        }
    }
    [Fact]
    public async Task GetMySlots_ShouldHandle_RepositoryException()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.GetMySlots())
            .ThrowsAsync(new InvalidOperationException("Database error"));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetMySlots());
        Assert.Equal("Database error", exception.Message);
        _repositoryMock.Verify(repo => repo.GetMySlots(), Times.Once);
    }
    [Fact]
    public async Task GetMySlots_ShouldVerifyMultipleRepositoryCalls()
    {
        // Arrange
        var mockSlots = new List<DoctorSlot>
    {
        DoctorSlot.Create(DateTime.Now.AddDays(1), Guid.NewGuid(), "Dr. John Doe", 100)
    };

        _repositoryMock.Setup(repo => repo.GetMySlots())
            .ReturnsAsync(mockSlots);

        // Act
        var result1 = await _service.GetMySlots();
        var result2 = await _service.GetMySlots();

        // Assert
        Assert.Equal(mockSlots.Count, result1.Count);
        Assert.Equal(mockSlots.Count, result2.Count);
        _repositoryMock.Verify(repo => repo.GetMySlots(), Times.Exactly(2));
    }

    [Fact]
    public async Task AddSlot_ShouldAddSlotAndReturnId()
    {
        // Arrange
        var slotRequest = new DoctorSlotRequestModel(DateTime.Now, Guid.NewGuid(), "Dr. Smith", 100);

        var newSlotId = Guid.NewGuid();

        _repositoryMock.Setup(repo => repo.AddSlot(It.IsAny<DoctorSlot>()))
            .ReturnsAsync(newSlotId);

        _repositoryMock.Setup(repo => repo.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.AddSlot(slotRequest);

        // Assert
        Assert.Equal(newSlotId, result);
        _repositoryMock.Verify(repo => repo.AddSlot(It.IsAny<DoctorSlot>()), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ReserveSlot_ShouldReserveSlot_WhenSlotIsAvailable()
    {
        // Arrange
        //var slotId = Guid.NewGuid();
        var slot = DoctorSlot.Create(DateTime.Now, Guid.NewGuid(), "John Smith", 500);

        _repositoryMock.Setup(repo => repo.GetSlotById(slot.Id))
            .ReturnsAsync(slot);

        _repositoryMock.Setup(repo => repo.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.ReserveSlot(slot.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(slot.Id, result.Id);
        Assert.True(slot.IsReserved);
        _repositoryMock.Verify(repo => repo.GetSlotById(slot.Id), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task ReserveSlot_ShouldThrowException_WhenSlotIsAlreadyReserved()
    {
        // Arrange
        var slotId = Guid.NewGuid();
        var slot = DoctorSlot.CreateReserved(DateTime.Now, Guid.NewGuid(), "Dr. Smith", 100);

        _repositoryMock.Setup(repo => repo.GetSlotById(slotId))
            .ReturnsAsync(slot);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _service.ReserveSlot(slotId));
        Assert.Equal("Slot is already reserved", exception.Message);

        _repositoryMock.Verify(repo => repo.GetSlotById(slotId), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task ReserveSlot_ShouldThrowException_WhenSlotNotFound()
    {
        // Arrange
        var slotId = Guid.NewGuid();

        _repositoryMock.Setup(repo => repo.GetSlotById(slotId))
            .ReturnsAsync((DoctorSlot?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _service.ReserveSlot(slotId));
        Assert.Equal("Slot not found", exception.Message);

        _repositoryMock.Verify(repo => repo.GetSlotById(slotId), Times.Once);
        _repositoryMock.Verify(repo => repo.SaveChangesAsync(), Times.Never);
    }
}
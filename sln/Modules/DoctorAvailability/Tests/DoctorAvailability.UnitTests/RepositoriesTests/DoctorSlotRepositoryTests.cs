using DoctorAvailability.Data.DbContext;
using DoctorAvailability.Business.Repositories;
using DoctorAvailability.Internal.Data.DbContext;
using DoctorAvailability.Internal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.UnitTests.RepositoriesTests;

// Chicago School "Classicist"
public class DoctorSlotRepositoryTests
{
    private DoctorAvailabilityContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DoctorAvailabilityContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB for each test
            .Options;

        return new DoctorAvailabilityContext(options);
    }

    [Fact]
    public async Task GetDoctorAvailableSlots_ShouldReturn_UnreservedSlots()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var doctorId = Guid.NewGuid();
        var doctorName = "Dr. John Doe";
        context.DoctorSlots.AddRange(
            DoctorSlot.Create(DateTime.Now.AddDays(1), doctorId, doctorName, 100),
            DoctorSlot.CreateReserved(DateTime.Now.AddDays(1), doctorId, doctorName, 150));
        await context.SaveChangesAsync();

        // Act
        var doctorAvailableSlots = await repository.GetDoctorAvailableSlots();

        // Assert
        Assert.Single(doctorAvailableSlots); // Only 1 slot is unreserved
        Assert.False(doctorAvailableSlots.First().IsReserved);
    }

    [Fact]
    public async Task AddSlot_ShouldAdd_NewSlot()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var newSlot = DoctorSlot.Create(DateTime.Now.AddDays(3), Guid.NewGuid(), "John Doe", 150);

        // Act
        var result = await repository.AddSlot(newSlot);

        // Assert
        var addedSlot = await context.DoctorSlots.FindAsync(result);
        Assert.NotNull(addedSlot);
        Assert.Equal(newSlot.Id, addedSlot.Id);
    }

    [Fact]
    public async Task GetMySlots_ShouldReturn_AllSlots()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var doctorId = Guid.NewGuid();
        var doctorName = "Dr. John Doe";
        context.DoctorSlots.AddRange(
            DoctorSlot.Create(DateTime.Now.AddDays(3), doctorId, doctorName, 150),
            DoctorSlot.CreateReserved(DateTime.Now.AddDays(3), doctorId, doctorName, 150)
        );
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetMySlots();

        // Assert
        Assert.Equal(2, result.Count); // All slots should be returned
    }

    [Fact]
    public async Task GetSlotById_ShouldReturn_CorrectSlot()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var slot = DoctorSlot.Create(DateTime.Now.AddDays(3), Guid.NewGuid(), "John Doe", 150);
        context.DoctorSlots.Add(slot);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetSlotById(slot.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(slot.Id, result.Id);
    }

    [Fact]
    public async Task GetSlotById_ShouldReturn_Null_WhenSlotDoesNotExist()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        // Act
        var result = await repository.GetSlotById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddSlot_ShouldThrow_WhenDoctorNameIsNull()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => repository.AddSlot(DoctorSlot.Create(DateTime.Now, Guid.NewGuid(), null, 100)));
    }
}

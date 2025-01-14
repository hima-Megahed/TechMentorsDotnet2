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

    //[Fact]
    //public async Task AddSlot_ShouldThrowException_WhenDuplicateId()
    //{
    //    // Arrange
    //    var context = CreateInMemoryContext();
    //    var repository = new DoctorSlotRepository(context);
    //    var duplicateId = Guid.NewGuid();

    //    var slot1 = DoctorSlot.Create(DateTime.Now.AddDays(3), duplicateId, "Doctor 1", 150);
    //    var slot2 = DoctorSlot.Create(DateTime.Now.AddDays(4), duplicateId, "Doctor 1", 200);

    //    await context.DoctorSlots.AddAsync(slot1);
    //    await context.SaveChangesAsync();

    //    // Act & Assert
    //    await Assert.ThrowsAsync<DbUpdateException>(() => repository.AddSlot(slot2));
    //}

    //[Fact]
    //public async Task AddSlot_ShouldThrowException_WhenDataIsIncomplete()
    //{
    //    // Arrange
    //    var context = CreateInMemoryContext();
    //    var repository = new DoctorSlotRepository(context);

    //    // Example of missing required fields (if applicable)
    //    var invalidSlot = DoctorSlot.Create(DateTime.MinValue, default, "", 0);
        

    //    // Act & Assert
    //    await Assert.ThrowsAsync<DbUpdateException>(() => repository.AddSlot(invalidSlot));
    //}

    //[Fact]
    //public async Task AddSlot_ShouldNotChangeDatabaseState_OnException()
    //{
    //    // Arrange
    //    var context = CreateInMemoryContext();
    //    var repository = new DoctorSlotRepository(context);

    //    var invalidSlot = DoctorSlot.Create(DateTime.Now, Guid.NewGuid(), "", 0);
       

    //    // Act
    //    try
    //    {
    //        await repository.AddSlot(invalidSlot);
    //    }
    //    catch
    //    {
    //        // Ignore the exception
    //    }

    //    // Assert
    //    Assert.Empty(context.DoctorSlots);
    //}

    [Fact]
    public async Task AddSlot_ShouldReturn_CorrectSlotId()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);
        var newSlot = DoctorSlot.Create(DateTime.Now.AddDays(2), Guid.NewGuid(), "DoctorName1", 250);

        // Act
        var result = await repository.AddSlot(newSlot);

        // Assert
        Assert.Equal(newSlot.Id, result);
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
    public async Task GetMySlots_ShouldReturn_EmptyList_WhenNoSlotsExist()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        // Act
        var result = await repository.GetMySlots();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result); // No slots should exist
    }

    [Fact]
    public async Task GetMySlots_ShouldReturn_AllSlotsWithNoFiltering()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var doctorId = Guid.NewGuid();
        var doctorName = "Doctor Name ";
        context.DoctorSlots.AddRange(
            DoctorSlot.Create(DateTime.Now.AddDays(1), doctorId, doctorName, 100),
            DoctorSlot.CreateReserved(DateTime.Now.AddDays(2), doctorId, doctorName, 200)
        );
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetMySlots();

        // Assert
        Assert.Equal(2, result.Count); // All slots should be returned
    }
    [Fact]
    public async Task GetMySlots_ShouldHandle_LargeDataSet()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var doctorId = Guid.NewGuid();
        var doctorName = "Doctor  Large Dataset";
        for (int i = 0; i < 1000; i++)
        {
            context.DoctorSlots.Add(DoctorSlot.Create(DateTime.Now.AddDays(i), doctorId, doctorName, 150 + i));
        }
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetMySlots();

        // Assert
        Assert.Equal(1000, result.Count); // Should handle large datasets correctly
    }

    [Fact]
    public async Task GetSlotById_ShouldHandle_ConcurrentRequests()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var slot = DoctorSlot.Create(DateTime.Now.AddDays(3), Guid.NewGuid(), "Concurrent Doctor", 150);
        context.DoctorSlots.Add(slot);
        await context.SaveChangesAsync();

        var slotId = slot.Id;

        // Act
        var tasks = new List<Task<DoctorSlot?>>();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(repository.GetSlotById(slotId));
        }
        var results = await Task.WhenAll(tasks);

        // Assert
        Assert.All(results, result =>
        {
            Assert.NotNull(result);
            Assert.Equal(slotId, result.Id);
        });
    }


    //[Fact]
    //public async Task GetMySlots_ShouldNotTrack_Entities()
    //{
    //    // Arrange
    //    var context = CreateInMemoryContext();
    //    var repository = new DoctorSlotRepository(context);

    //    var slot = DoctorSlot.Create(DateTime.Now.AddDays(1), Guid.NewGuid(), "Doctor name ", 100);
    //    context.DoctorSlots.Add(slot);
    //    await context.SaveChangesAsync();

    //    // Act
    //    var result = await repository.GetMySlots();

    //    // Assert
    //    Assert.False(context.Entry(result.First()).IsKeySet); // AsNoTracking ensures no tracking
    //}

    [Fact]
    public async Task GetMySlots_ShouldNotModifyDatabaseState()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var slot = DoctorSlot.Create(DateTime.Now.AddDays(1), Guid.NewGuid(), "Doctore Static", 100);
        context.DoctorSlots.Add(slot);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetMySlots();

        // Assert
        Assert.Single(await context.DoctorSlots.ToListAsync());
        Assert.Equal(slot.Id, result.First().Id); // Data integrity is maintained
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
    public async Task GetSlotById_ShouldReturn_CorrectSlot_WhenMultipleSlotsExist()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        var slot1 = DoctorSlot.Create(DateTime.Now.AddDays(3), Guid.NewGuid(), "Doctor name 1 ", 150);
        var slot2 = DoctorSlot.Create(DateTime.Now.AddDays(5), Guid.NewGuid(), "Doctor name 2", 200);
        context.DoctorSlots.AddRange(slot1, slot2);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetSlotById(slot2.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(slot2.Id, result.Id);
        Assert.Equal(slot2.DoctorName, result.DoctorName);
    }
    [Fact]
    public async Task GetSlotById_ShouldHandle_InvalidSlotIdFormat()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await repository.GetSlotById(Guid.Empty); // Invalid GUID
        });
    }


    [Fact]
    public async Task AddSlot_ShouldThrow_WhenDoctorNameIsNull()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var repository = new DoctorSlotRepository(context);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            repository.AddSlot(DoctorSlot.Create(DateTime.Now, Guid.NewGuid(), null, 100)));
    }
}
using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Infrastructure.Persistence.DbContext;
using AppointmentBooking.Infrastructure.Repositories;
using AppointmentBooking.Shared.Gateways.DoctorAvailability;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shared.Domain.Enums;
using Shared.DTOs.DoctorAvailability;

namespace AppointmentBooking.UnitTests.RepositoriesTests;

// Chicago School "Classicist"
public class AppointmentRepositoryTests
{
    private AppointmentBookingContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppointmentBookingContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppointmentBookingContext(options);
    }

    [Fact]
    public async Task BookAppointment_ShouldAddAppointmentAndReserveSlot()
    {
        // Arrange
        await using var context = CreateInMemoryContext();
        var doctorAvailabilityGatewayMock = new Mock<IDoctorAvailabilityGateway>();
        var repository = new AppointmentRepository(context, doctorAvailabilityGatewayMock.Object);

        var slotId = Guid.NewGuid();
        var patientName = "John Doe";
        var command = new BookAppointmentCommand { SlotId = slotId, PatientName = patientName };

        doctorAvailabilityGatewayMock.Setup(d => d.ReserveSlot(slotId))
            .Returns(Task.FromResult(
                new SlotDto
                {
                    Id = slotId,
                    Cost = 100,
                    Date = DateTime.UtcNow,
                    DoctorId = Guid.NewGuid(),
                    DoctorName = "John Doe",
                    IsReserved = false
                }));

        // Act
        var result = await repository.BookAppointment(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(patientName, result.PatientName);
        Assert.Equal(slotId, result.SlotId);
        Assert.Single(context.Appointments);
        doctorAvailabilityGatewayMock.Verify(d => d.ReserveSlot(slotId), Times.Once);
    }

    [Fact]
    public async Task GetUpcomingAppointments_ShouldReturnListOfAppointments()
    {
        // Arrange
        await using var context = CreateInMemoryContext();
        var repository = new AppointmentRepository(context, Mock.Of<IDoctorAvailabilityGateway>());

        var appointments = new List<Appointment>
        {
            Appointment.Create(Guid.NewGuid(), "John Doe"),
            Appointment.Create(Guid.NewGuid(), "John Doe")
        };

        context.Appointments.AddRange(appointments);
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetUpcomingAppointments();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task ChangeAppointmentStatus_ShouldUpdateStatusAndSaveChanges()
    {
        // Arrange
        await using var context = CreateInMemoryContext();
        var repository = new AppointmentRepository(context, Mock.Of<IDoctorAvailabilityGateway>());
        
        var newStatus = AppointmentStatus.Completed;
        var appointment = Appointment.Create(Guid.NewGuid(), "John Doe");

        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();

        // Act
        await repository.ChangeAppointmentStatus(appointment.Id, newStatus);

        // Assert
        var updatedAppointment = await context.Appointments.FindAsync(appointment.Id);
        Assert.NotNull(updatedAppointment);
        Assert.Equal(newStatus, updatedAppointment.Status);
    }

    [Fact]
    public async Task ChangeAppointmentStatus_ShouldThrowKeyNotFoundExceptionIfAppointmentNotFound()
    {
        // Arrange
        await using var context = CreateInMemoryContext();
        var repository = new AppointmentRepository(context, Mock.Of<IDoctorAvailabilityGateway>());

        var appointmentId = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            await repository.ChangeAppointmentStatus(appointmentId, AppointmentStatus.Completed));
    }
}
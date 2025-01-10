using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Application.BookAppointment.DTOs;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Infrastructure.Persistence.DbContext;
using DoctorAvailability.Shared.Facade;

namespace AppointmentBooking.Infrastructure.Repositories;

public class AppointmentRepository(
    AppointmentBookingContext context,
    IDoctorAvailabilityFacade doctorAvailabilityFacade) : IAppointmentRepository
{
    public async Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand)
    {
        var appointmentEntity = context.Add(Appointment.Create(bookAppointmentCommand.SlotId,
            bookAppointmentCommand.PatientName));
        await doctorAvailabilityFacade.ReserveSlot(bookAppointmentCommand.SlotId);
        await context.SaveChangesAsync();
        return new AppointmentDto
        {
            PatientName = appointmentEntity.Entity.PatientName,
            Id = appointmentEntity.Entity.Id,
            ReservedAt = appointmentEntity.Entity.ReservedAt,
            SlotId = appointmentEntity.Entity.SlotId,
            PatientId = appointmentEntity.Entity.PatientId,
            Status = appointmentEntity.Entity.Status.ToString(),
        };
    }
}
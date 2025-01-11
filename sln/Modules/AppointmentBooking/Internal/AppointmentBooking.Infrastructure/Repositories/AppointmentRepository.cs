using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Infrastructure.Persistence.DbContext;
using AppointmentBooking.Shared.Gateways.DoctorAvailability;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace AppointmentBooking.Infrastructure.Repositories;

public class AppointmentRepository(
    AppointmentBookingContext context,
    IDoctorAvailabilityGateway doctorAvailabilityGateway) : IAppointmentRepository
{
    public async Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand)
    {
        var appointmentEntity = context.Add(Appointment.Create(bookAppointmentCommand.SlotId,
            bookAppointmentCommand.PatientName));
        await doctorAvailabilityGateway.ReserveSlot(bookAppointmentCommand.SlotId);
        await context.SaveChangesAsync();
        return new AppointmentDto
        {
            PatientName = appointmentEntity.Entity.PatientName,
            Id = appointmentEntity.Entity.Id,
            ReservedAt = appointmentEntity.Entity.ReservedAt,
            SlotId = appointmentEntity.Entity.SlotId,
            PatientId = appointmentEntity.Entity.PatientId,
            Status = appointmentEntity.Entity.Status,
        };
    }

    public async Task<List<AppointmentDto>> GetUpcomingAppointments()
    {
        return await context.Appointments.Select(a => new AppointmentDto
        {
            PatientName = a.PatientName,
            Id = a.Id,
            ReservedAt = a.ReservedAt,
            SlotId = a.SlotId,
            Status = a.Status,
            PatientId = a.PatientId,
        }).ToListAsync();
    }

    public async Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status)
    {
        var appointment = await context.Appointments.FindAsync(appointmentId);
        if (appointment is null) throw new KeyNotFoundException($"Appointment with ID: {appointmentId} not found");
        
        appointment.SetStatus(status);
        await context.SaveChangesAsync();
    }
}
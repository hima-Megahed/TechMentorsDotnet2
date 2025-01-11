using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Application.Contracts.Services;
using AppointmentBooking.Infrastructure.Repositories;
using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace AppointmentBooking.Infrastructure.Services;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
    public async Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand)
    {
        return await appointmentRepository.BookAppointment(bookAppointmentCommand);
    }

    public async Task<List<AppointmentDto>> GetUpcomingAppointments()
    {
        return await appointmentRepository.GetUpcomingAppointments();
    }

    public async Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status)
    {
        await appointmentRepository.ChangeAppointmentStatus(appointmentId, status);
    }
}
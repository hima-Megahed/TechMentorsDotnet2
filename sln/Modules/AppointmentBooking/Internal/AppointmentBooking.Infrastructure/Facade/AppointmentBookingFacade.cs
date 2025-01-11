using AppointmentBooking.Application.Contracts.Services;
using AppointmentBooking.Shared.Facade;
using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace AppointmentBooking.Infrastructure.Facade;

public class AppointmentBookingFacade(IAppointmentService appointmentService) : IAppointmentBookingFacade
{
    public async Task<List<AppointmentDto>> GetUpcomingAppointments()
    {
        return await appointmentService.GetUpcomingAppointments();
    }

    public async Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status)
    {
        await appointmentService.ChangeAppointmentStatus(appointmentId, status);
    }
}
using AppointmentBooking.Shared.Facade;
using DoctorAppointmentManagement.Application.Ports.Driven;
using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace DoctorAppointmentManagement.Infrastructure.Gateways;

public class AppointmentBookingGateway(IAppointmentBookingFacade appointmentBookingFacade) : IAppointmentBookingGateway
{
    public async Task<List<AppointmentDto>> GetUpcomingAppointments()
    {
        return await appointmentBookingFacade.GetUpcomingAppointments();
    }

    public async Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status)
    {
        await appointmentBookingFacade.ChangeAppointmentStatus(appointmentId, status);
    }
}
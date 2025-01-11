using DoctorAppointmentManagement.Application.Ports.Driven;
using DoctorAppointmentManagement.Application.Ports.Driving;
using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace DoctorAppointmentManagement.Application.Usecases;

public class AppointmentManagementService(IAppointmentBookingGateway appointmentBookingGateway) : IAppointmentManagementService
{
    public async Task<List<AppointmentDto>> GetUpcomingAppointments()
    {
        return await appointmentBookingGateway.GetUpcomingAppointments();
    }

    public async Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status)
    {
        await appointmentBookingGateway.ChangeAppointmentStatus(appointmentId, status);
    }
}
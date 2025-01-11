using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace DoctorAppointmentManagement.Application.Ports.Driven;

public interface IAppointmentBookingGateway
{
    Task<List<AppointmentDto>> GetUpcomingAppointments();
    Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status); 
}
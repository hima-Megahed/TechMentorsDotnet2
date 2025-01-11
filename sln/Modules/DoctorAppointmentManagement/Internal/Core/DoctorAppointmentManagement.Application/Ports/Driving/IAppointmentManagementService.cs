using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace DoctorAppointmentManagement.Application.Ports.Driving;

public interface IAppointmentManagementService
{
    Task<List<AppointmentDto>> GetUpcomingAppointments();
    Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status); 
}
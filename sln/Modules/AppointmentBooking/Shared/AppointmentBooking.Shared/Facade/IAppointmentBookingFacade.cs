using Shared.Domain.Enums;
using Shared.DTOs;
using Shared.DTOs.AppointmentBooking;

namespace AppointmentBooking.Shared.Facade;

public interface IAppointmentBookingFacade
{
    Task<List<AppointmentDto>> GetUpcomingAppointments();
    Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status); 
}
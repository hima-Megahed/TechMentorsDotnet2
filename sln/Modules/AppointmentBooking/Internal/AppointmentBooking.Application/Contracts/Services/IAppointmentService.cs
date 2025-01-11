using AppointmentBooking.Application.BookAppointment.Commands;
using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace AppointmentBooking.Application.Contracts.Services;

public interface IAppointmentService
{
    Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand);
    Task<List<AppointmentDto>> GetUpcomingAppointments();
    Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status); 
}
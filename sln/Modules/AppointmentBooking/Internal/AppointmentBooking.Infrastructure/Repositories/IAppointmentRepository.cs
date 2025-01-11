using AppointmentBooking.Application.BookAppointment.Commands;
using Shared.Domain.Enums;
using Shared.DTOs.AppointmentBooking;

namespace AppointmentBooking.Infrastructure.Repositories;

public interface IAppointmentRepository
{
    Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand);
    Task<List<AppointmentDto>> GetUpcomingAppointments();
    Task ChangeAppointmentStatus(Guid appointmentId, AppointmentStatus status); 
}
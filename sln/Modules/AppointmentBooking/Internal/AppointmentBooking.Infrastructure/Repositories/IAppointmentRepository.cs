using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Application.BookAppointment.DTOs;

namespace AppointmentBooking.Infrastructure.Repositories;

public interface IAppointmentRepository
{
    Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand);
}
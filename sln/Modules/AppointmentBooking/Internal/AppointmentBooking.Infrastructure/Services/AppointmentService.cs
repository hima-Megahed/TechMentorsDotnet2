using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Application.BookAppointment.DTOs;
using AppointmentBooking.Application.Contracts.Services;
using AppointmentBooking.Infrastructure.Repositories;

namespace AppointmentBooking.Infrastructure.Services;

public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
{
    public async Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand)
    {
        return await appointmentRepository.BookAppointment(bookAppointmentCommand);
    }
}
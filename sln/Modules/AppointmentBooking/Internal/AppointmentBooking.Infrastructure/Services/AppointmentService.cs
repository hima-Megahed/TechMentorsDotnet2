using AppointmentBooking.Application.BookAppointment.DTOs;
using AppointmentBooking.Application.Contracts.Services;

namespace AppointmentBooking.Infrastructure.Services;

public class AppointmentService : IAppointmentService
{
    public Task<AppointmentDto> BookAppointment()
    {
        throw new NotImplementedException();
    }
}
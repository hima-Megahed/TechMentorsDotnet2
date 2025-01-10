using AppointmentBooking.Application.Contracts.Services;
using MediatR;

namespace AppointmentBooking.Application.BookAppointment.Commands;

public class BookAppointmentCommand : IRequest
{
    public Guid SlotId { get; init; }
    public required string PatientName { get; init; }
}

public class BookAppointmentCommandHandler(
    IAppointmentService appointmentService) : IRequestHandler<BookAppointmentCommand>
{
    public async Task Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
    {
        await appointmentService.BookAppointment(request);
    }
}
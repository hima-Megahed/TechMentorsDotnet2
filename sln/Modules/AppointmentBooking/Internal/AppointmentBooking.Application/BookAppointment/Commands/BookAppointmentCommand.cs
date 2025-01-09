using MediatR;

namespace AppointmentBooking.Application.BookAppointment.Commands;

public abstract class BookAppointmentCommand : IRequest
{
    public Guid Id { get; init; }
    public Guid SlotId { get; init; }
    public Guid PatientId { get; init; }
    public required string PatientName { get; init; }
    public DateTime ReservedAt { get; init; }
    public AppointmentStatus Status { get; init; }

    public enum AppointmentStatus
    {
        New = 0,
        Completed = 1,
        Canceled = 2
    }
}

public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand>
{
    public async Task Handle(BookAppointmentCommand request, CancellationToken cancellationToken)
    {
        
    }
}
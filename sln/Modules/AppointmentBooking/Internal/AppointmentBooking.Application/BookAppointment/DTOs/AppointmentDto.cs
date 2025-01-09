using AppointmentBooking.Application.BookAppointment.Commands;

namespace AppointmentBooking.Application.BookAppointment.DTOs;

public class AppointmentDto
{
    public Guid Id { get; init; }
    public Guid SlotId { get; init; }
    public Guid PatientId { get; init; }
    public required string PatientName { get; init; }
    public DateTime ReservedAt { get; init; }
    public BookAppointmentCommand.AppointmentStatus Status { get; init; }
}
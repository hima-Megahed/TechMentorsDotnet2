using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Domain.Entities;

public class Appointment
{
    public Appointment()
    {
        
    }
    public Guid Id { get; init; }
    public Guid SlotId { get; init; }
    public Guid PatientId { get; init; }
    public required string PatientName { get; init; }
    public DateTime ReservedAt { get; init; }
    public AppointmentStatus Status { get; init; }
}
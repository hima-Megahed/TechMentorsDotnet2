using AppointmentBooking.Domain.Enums;

namespace AppointmentBooking.Domain.Entities;

public class Appointment(Guid slotId, string patientName)
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid SlotId { get; init; } = slotId;
    public Guid PatientId { get; init; } = Guid.NewGuid();
    public string PatientName { get; init; } = patientName;
    public DateTime ReservedAt { get; init; } = DateTime.UtcNow;
    public AppointmentStatus Status { get; init; } = AppointmentStatus.New;
}
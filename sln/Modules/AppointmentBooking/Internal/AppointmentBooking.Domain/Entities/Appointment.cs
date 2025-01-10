using AppointmentBooking.Domain.Enums;
using Shared.Domain.Entities;
using Shared.DomainEvents;
using Shared.DomainEvents.Events;
using Shared.DomainEvents.Events.DTOs;

namespace AppointmentBooking.Domain.Entities;

public class Appointment : BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid SlotId { get; private set;}
    public Guid PatientId { get; private set; } = Guid.NewGuid();
    public string PatientName { get; private set; }
    public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.New;

    private Appointment(Guid slotId, string patientName)
    {
        SlotId = slotId;
        PatientName = patientName;
    }
    public static Appointment Create(Guid slotId, string patientName)
    {
        ArgumentException.ThrowIfNullOrEmpty(patientName);

        var appointment = new Appointment(slotId, patientName);
        appointment.AddDomainEvent(new AppointmentBookedDomainEvent(new AppointmentDto
        {
            SlotId = slotId,
            PatientName = patientName,
            ReservedAt = appointment.ReservedAt,
            PatientId = appointment.PatientId
        }));
        return appointment;
    }
}
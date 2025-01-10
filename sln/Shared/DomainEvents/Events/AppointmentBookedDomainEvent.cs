using Shared.DomainEvents.Events.DTOs;

namespace Shared.DomainEvents.Events;

public record AppointmentBookedDomainEvent(AppointmentDto Appointment) : IDomainEvent
{ }
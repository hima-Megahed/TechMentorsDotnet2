using Shared.DomainEvents;
using Shared.DTOs.AppointmentBooking;

namespace Shared.DTOs;

public record AppointmentBookedDomainEvent(AppointmentDto Appointment) : IDomainEvent
{ }
using DoctorAvailability.Business.Services.DoctorSlot;
using MediatR;
using Shared.DomainEvents.Events;

namespace DoctorAvailability.Business.EventHandlers;

public class AppointmentBookedEventHandler(IDoctorSlotService doctorSlotService)
    : INotificationHandler<AppointmentBookedDomainEvent>
{
    public async Task Handle(AppointmentBookedDomainEvent notification, CancellationToken cancellationToken)
    {
        await doctorSlotService.ReserveSlot(notification.Appointment.SlotId);
    }
}
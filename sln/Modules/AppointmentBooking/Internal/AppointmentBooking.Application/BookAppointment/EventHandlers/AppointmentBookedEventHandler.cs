using DoctorAvailability.Shared.Facade;
using MassTransit;
using MediatR;
using Shared.DomainEvents.Events;
using Shared.IntegrationEvents;

namespace AppointmentBooking.Application.BookAppointment.EventHandlers;

public class AppointmentBookedEventHandler(IBus bus, IDoctorAvailabilityFacade doctorAvailabilityFacade)
    : INotificationHandler<AppointmentBookedDomainEvent>
{
    public async Task Handle(AppointmentBookedDomainEvent notification, CancellationToken cancellationToken)
    {
        var slot = await doctorAvailabilityFacade.GetSlotById(notification.Appointment.SlotId);
        await bus.Publish(new AppointmentBookedIntegrationEvent
        {
            PatientName = notification.Appointment.PatientName,
            AppointmentDate = slot.Date,
            DoctorName = slot.DoctorName,
        }, cancellationToken);
    }
}
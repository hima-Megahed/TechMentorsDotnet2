using AppointmentBooking.Shared.Gateways.DoctorAvailability;
using MassTransit;
using MediatR;
using Shared.DTOs;
using Shared.IntegrationEvents;

namespace AppointmentBooking.Application.BookAppointment.EventHandlers;

public class AppointmentBookedEventHandler(IBus bus, IDoctorAvailabilityGateway doctorAvailabilityGateway)
    : INotificationHandler<AppointmentBookedDomainEvent>
{
    public async Task Handle(AppointmentBookedDomainEvent notification, CancellationToken cancellationToken)
    {
        var slot = await doctorAvailabilityGateway.GetSlotById(notification.Appointment.SlotId);
        await bus.Publish(new AppointmentBookedIntegrationEvent
        {
            PatientName = notification.Appointment.PatientName,
            AppointmentDate = slot.Date,
            DoctorName = slot.DoctorName,
        }, cancellationToken);
    }
}
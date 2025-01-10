using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.IntegrationEvents;

namespace AppointmentConfirmation.Notification.IntegrationEventsHandler;

public class AppointmentBookedIntegrationEventHandler(
    ILogger<AppointmentBookedIntegrationEventHandler> logger) : IConsumer<AppointmentBookedIntegrationEvent>
{
    public Task Consume(ConsumeContext<AppointmentBookedIntegrationEvent> context)
    {
        var message =
            $"Hello {context.Message.PatientName}, " +
            $"your appointment with Dr. {context.Message.DoctorName} " +
            $"is on {context.Message.AppointmentDate.ToLongDateString()}. Please contact us if you have any questions.";
        logger.LogCritical(message);
        return Task.CompletedTask;
    }
}
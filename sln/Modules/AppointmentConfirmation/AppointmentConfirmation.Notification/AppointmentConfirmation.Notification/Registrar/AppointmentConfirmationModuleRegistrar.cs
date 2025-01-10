using System.Reflection;
using AppointmentConfirmation.Notification.IntegrationEventsHandler;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentConfirmation.Notification.Registrar;

public static class AppointmentConfirmationModuleRegistrar
{
    public static IServiceCollection AddDoctorAvailabilityModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AppointmentBookedIntegrationEventHandler>();
        return services;
    }

    public static Assembly[] GetModuleAssemblies()
    {
        return
        [
            typeof(AppointmentBookedIntegrationEventHandler).Assembly
        ];
    }
}
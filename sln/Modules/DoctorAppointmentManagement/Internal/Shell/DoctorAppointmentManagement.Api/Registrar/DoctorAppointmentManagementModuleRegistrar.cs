using System.Reflection;
using DoctorAppointmentManagement.Api.Endpoints.ViewMyAppointments;
using DoctorAppointmentManagement.Application.Ports.Driven;
using DoctorAppointmentManagement.Application.Ports.Driving;
using DoctorAppointmentManagement.Application.Usecases;
using DoctorAppointmentManagement.Domain;
using DoctorAppointmentManagement.Infrastructure.Gateways;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAppointmentManagement.Api.Registrar;

public static class DoctorAppointmentManagementModuleRegistrar
{
    public static IServiceCollection AddDoctorAppointmentManagementModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAppointmentBookingGateway, AppointmentBookingGateway>();
        services.AddScoped<IAppointmentManagementService, AppointmentManagementService>();

        return services;
    }

    public static Assembly[] GetModuleAssemblies()
    {
        return
        [
            typeof(ViewMyAppointments).Assembly,
            typeof(AppointmentBookingGateway).Assembly,
            typeof(AppointmentManagementService).Assembly,
            typeof(Class1).Assembly,
        ];
    }
}
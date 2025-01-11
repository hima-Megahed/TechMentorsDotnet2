using System.Reflection;
using AppointmentBooking.Api.Endpoints.BookAppointment;
using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Application.Contracts.Services;
using AppointmentBooking.Application.GetAvailableSlots.Queries;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Infrastructure.Facade;
using AppointmentBooking.Infrastructure.Gateways.DoctorAvailability;
using AppointmentBooking.Infrastructure.Persistence.DbContext;
using AppointmentBooking.Infrastructure.Repositories;
using AppointmentBooking.Infrastructure.Services;
using AppointmentBooking.Shared.Facade;
using AppointmentBooking.Shared.Gateways.DoctorAvailability;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Domain.Interceptors;

namespace AppointmentBooking.Infrastructure.Registrar;

public static class AppointmentBookingModuleRegistrar
{
    public static IServiceCollection AddAppointmentBookingModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppointmentBookingContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlite(connectionString);
        });
        
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAvailableSlotsQuery).Assembly));

        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IDoctorAvailabilityGateway, DoctorAvailabilityGateway>();
        services.AddScoped<IAppointmentBookingFacade, AppointmentBookingFacade>();

        return services;
    }
    
    public static Assembly[] GetModuleAssemblies()
    {
        return
        [
            typeof(BookAppointmentEndpoint).Assembly,
            typeof(AppointmentService).Assembly,
            typeof(BookAppointmentCommand).Assembly,
            typeof(Appointment).Assembly
        ];
    }
}
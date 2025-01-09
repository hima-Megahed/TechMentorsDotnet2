using AppointmentBooking.Application.Contracts.Repositories;
using AppointmentBooking.Application.GetAvailableSlots.Queries;
using AppointmentBooking.Infrastructure.Persistence.DbContext;
using AppointmentBooking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure.Registrar;

public static class AppointmentBookingModuleRegistrar
{
    public static IServiceCollection AddAppointmentBookingModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppointmentBookingContext>(options => { options.UseSqlite(connectionString); });

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAvailableSlotsQuery).Assembly));

        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        return services;
    }
}
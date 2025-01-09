using AppointmentBooking.Infrastructure.Persistence.DbContext;
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
        
        services.AddDbContext<AppointmentBookingContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        
        return services;
    }
}

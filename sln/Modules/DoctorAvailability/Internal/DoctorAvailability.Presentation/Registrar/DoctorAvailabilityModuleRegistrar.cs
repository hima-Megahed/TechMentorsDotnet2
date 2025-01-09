using DoctorAvailability.Business.Repositories;
using DoctorAvailability.Business.Services.DoctorSlot;
using DoctorAvailability.Internal.Data.DbContext;
using DoctorAvailability.Internal.Data.Models;
using DoctorAvailability.Shared.Facade;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAvailability.Presentation.Registrar;
public static class DoctorAvailabilityModuleRegistrar
{
    public static IServiceCollection AddDoctorAvailabilityModule(this IServiceCollection services,
       IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        services.AddDbContext<DoctorAvailabilityContext>(options =>
        {
            options.UseSqlite(connectionString);
        });

        services.AddScoped<IDoctorSlotRepository, DoctorSlotRepository>();
        services.AddScoped<IDoctorSlotService, DoctorSlotService>();
        services.AddScoped<IDoctorAvailability, Business.Facade.DoctorAvailability>();

        return services;
    }

    public static void UseDoctorAvailabilityDbInitializer(this WebApplication app)
    {
        // Seed data during startup
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DoctorAvailabilityContext>();

        // Ensure database is created
        context.Database.EnsureCreated();

        // Seed data if necessary
        if (!context.DoctorSlots.Any())
        {
            var doctorId = Guid.NewGuid();
            var doctorName = "Magdy Yaaquob";
            context.DoctorSlots.AddRange(
                DoctorSlot.Create(DateTime.Now.AddDays(2), doctorId, doctorName, 100),
                DoctorSlot.Create(DateTime.Now.AddDays(5), doctorId, doctorName, 100),
                DoctorSlot.Create(DateTime.Now.AddDays(8), doctorId, doctorName, 100)
            );
            context.SaveChanges();
        }
    }
}

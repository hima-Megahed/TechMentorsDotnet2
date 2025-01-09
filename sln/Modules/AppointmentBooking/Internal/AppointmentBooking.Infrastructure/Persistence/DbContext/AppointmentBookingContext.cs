using System.Reflection;
using AppointmentBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Persistence.DbContext;

public class AppointmentBookingContext(DbContextOptions<AppointmentBookingContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("AppointmentBooking");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
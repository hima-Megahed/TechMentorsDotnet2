using System.Reflection;
using DoctorAvailability.Internal.Data.Models;

namespace DoctorAvailability.Internal.Data.DbContext;
public class DoctorAvailabilityContext(DbContextOptions<DoctorAvailabilityContext> options)
    : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<DoctorSlot> DoctorSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("DoctorAvailability");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

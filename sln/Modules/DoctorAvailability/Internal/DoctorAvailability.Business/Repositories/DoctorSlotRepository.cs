using DoctorAvailability.Internal.Data.DbContext;
using DoctorAvailability.Internal.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.Business.Repositories;
public class DoctorSlotRepository(DoctorAvailabilityContext context) : IDoctorSlotRepository
{
    public async Task<List<DoctorSlot>> GetDoctorAvailableSlots()
    {
        return await context
            .DoctorSlots
            .Where(s => !s.IsReserved)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Guid> AddSlot(DoctorSlot slot)
    {
        context.DoctorSlots.Add(slot);
        await context.SaveChangesAsync();
        return slot.Id;
    }

    public async Task<List<DoctorSlot>> GetMySlots()
    {
        return await context
            .DoctorSlots
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<DoctorSlot?> GetSlotById(Guid slotId)
    {
        return await context
            .DoctorSlots.FindAsync(slotId);
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}



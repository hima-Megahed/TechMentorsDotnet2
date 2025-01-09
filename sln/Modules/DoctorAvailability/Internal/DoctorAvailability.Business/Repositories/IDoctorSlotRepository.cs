using DoctorAvailability.Internal.Data.Models;

namespace DoctorAvailability.Business.Repositories;

public interface IDoctorSlotRepository
{
    Task<List<DoctorSlot>> GetDoctorAvailableSlots();
    Task<Guid> AddSlot(DoctorSlot slot);
    Task<List<DoctorSlot>> GetMySlots();
    Task<DoctorSlot?> GetSlotById(Guid slotId);
    Task SaveChangesAsync();
}
using Shared.DTOs.DoctorAvailability;

namespace DoctorAvailability.Shared.Facade;

public interface IDoctorAvailabilityFacade
{
    Task<IList<SlotDto>> GetDoctorAvailableSlots();
    Task<SlotDto> ReserveSlot(Guid slotId);
    Task<SlotDto> GetSlotById(Guid slotId);
}
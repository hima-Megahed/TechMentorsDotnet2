using DoctorAvailability.Shared.Models;

namespace DoctorAvailability.Shared.Facade;

public interface IDoctorAvailability
{
    Task<IList<SlotDto>> GetDoctorAvailableSlots(Guid doctorId);
    Task<SlotDto> ReserveSlot(Guid slotId);
}
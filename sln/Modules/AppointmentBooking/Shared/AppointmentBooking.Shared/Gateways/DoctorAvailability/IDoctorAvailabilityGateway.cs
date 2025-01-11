using DoctorAvailability.Shared.Models;

namespace AppointmentBooking.Shared.Gateways.DoctorAvailability;

public interface IDoctorAvailabilityGateway
{
    Task<IList<SlotDto>> GetDoctorAvailableSlots();
    Task<SlotDto> ReserveSlot(Guid slotId);
    Task<SlotDto> GetSlotById(Guid slotId);
}
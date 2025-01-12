using AppointmentBooking.Shared.Gateways.DoctorAvailability;
using DoctorAvailability.Shared.Facade;
using Shared.DTOs.DoctorAvailability;

namespace AppointmentBooking.Infrastructure.Gateways.DoctorAvailability;

public class DoctorAvailabilityGateway(IDoctorAvailabilityFacade doctorAvailabilityFacade) : IDoctorAvailabilityGateway
{
    public async Task<IList<SlotDto>> GetDoctorAvailableSlots()
    {
        return await doctorAvailabilityFacade.GetDoctorAvailableSlots();
    }

    public async Task<SlotDto> ReserveSlot(Guid slotId)
    {
        return await doctorAvailabilityFacade.ReserveSlot(slotId);
    }

    public async Task<SlotDto> GetSlotById(Guid slotId)
    {
        return await doctorAvailabilityFacade.GetSlotById(slotId);
    }
}
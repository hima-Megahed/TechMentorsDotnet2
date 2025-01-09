using DoctorAvailability.Business.Services.DoctorSlot;
using DoctorAvailability.Shared.Facade;
using DoctorAvailability.Shared.Models;

namespace DoctorAvailability.Business.Facade;

public class DoctorAvailabilityFacade(IDoctorSlotService doctorSlotService) : IDoctorAvailabilityFacade
{
    public async Task<IList<SlotDto>> GetDoctorAvailableSlots()
    {
        var slots = await doctorSlotService.GetDoctorAvailableSlots();
        return slots.Select(x => new SlotDto
        {
            Id = x.Id,
            Date = x.Date,
            DoctorId = x.DoctorId,
            DoctorName = x.DoctorName,
            Cost = x.Cost
        }).ToList();
    }

    public async Task<SlotDto> ReserveSlot(Guid slotId)
    {
        return await doctorSlotService.ReserveSlot(slotId);
    }
}
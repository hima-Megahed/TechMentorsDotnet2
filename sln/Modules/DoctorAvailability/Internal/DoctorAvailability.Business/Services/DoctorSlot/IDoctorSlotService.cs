﻿using DoctorAvailability.Business.Services.DoctorSlot.Models;
using Shared.DTOs.DoctorAvailability;

namespace DoctorAvailability.Business.Services.DoctorSlot;

public interface IDoctorSlotService
{
    Task<List<Internal.Data.Models.DoctorSlot>> GetMySlots();
    Task<List<Internal.Data.Models.DoctorSlot>> GetDoctorAvailableSlots();
    Task<Guid> AddSlot(DoctorSlotRequestModel doctorSlotRequestModel);
    Task<SlotDto> ReserveSlot(Guid slotId);
    Task<SlotDto> GetSlotById(Guid slotId);
}
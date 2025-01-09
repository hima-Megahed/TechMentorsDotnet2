namespace DoctorAvailability.Business.Services.DoctorSlot.Models;
public record DoctorSlotRequestModel(DateTime Date, Guid DoctorId, string DoctorName, decimal Cost);


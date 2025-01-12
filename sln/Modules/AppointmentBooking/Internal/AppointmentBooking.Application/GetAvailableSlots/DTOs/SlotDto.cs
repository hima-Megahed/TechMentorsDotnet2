namespace AppointmentBooking.Application.GetAvailableSlots.DTOs;

public class SlotDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public bool IsReserved { get; set; }
    public decimal Cost { get; set; }

    public static IList<SlotDto> From(IList<global::Shared.DTOs.DoctorAvailability.SlotDto> slots)
    {
        return slots.Select(slot => new SlotDto
        {
            Id = slot.Id,
            Date = slot.Date,
            DoctorId = slot.DoctorId,
            DoctorName = slot.DoctorName,
            Cost = slot.Cost,
            IsReserved = slot.IsReserved
        }).ToList();
    }
}
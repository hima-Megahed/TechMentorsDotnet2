
namespace DoctorAvailability.Shared.Models;

public class SlotDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public bool IsReserved { get; set; }
    public decimal Cost { get; set; }
}
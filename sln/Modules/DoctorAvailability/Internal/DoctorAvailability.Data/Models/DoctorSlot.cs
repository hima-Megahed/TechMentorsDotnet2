using System.ComponentModel.DataAnnotations;

namespace DoctorAvailability.Internal.Data.Models;
public class DoctorSlot
{
    public Guid Id { get; private set; }
    public DateTime Date { get; private set; }
    public Guid DoctorId { get; private set; }
    [MaxLength(100)]
    public string DoctorName { get; private set; } = string.Empty;
    public bool IsReserved { get; private set; }
    public decimal Cost { get; private set; }
    private DoctorSlot() { }

    public static DoctorSlot Create(DateTime date, Guid doctorId, string doctorName, decimal cost)
    {
        if (date == default)
        {
            throw new ArgumentException("Date is required", nameof(date));
        }
        if (doctorId == default)
        {
            throw new ArgumentException("DoctorId is required", nameof(doctorId));
        }
        if (doctorName == string.Empty || doctorName == null)
        {
            throw new ArgumentException("DoctorName is required", nameof(doctorName));
        }
        if (cost == default)
        {
            throw new ArgumentException("Cost is required", nameof(cost));
        }

        return new DoctorSlot
        {
            Id = Guid.NewGuid(),
            Date = date,
            DoctorId = doctorId,
            DoctorName = doctorName,
            Cost = cost
        };
    }
    public static DoctorSlot CreateReserved(DateTime date, Guid doctorId, string doctorName, decimal cost)
    {
        if (date == default)
        {
            throw new ArgumentException("Date is required", nameof(date));
        }
        if (doctorId == default)
        {
            throw new ArgumentException("DoctorId is required", nameof(doctorId));
        }
        if (doctorName == string.Empty)
        {
            throw new ArgumentException("DoctorName is required", nameof(doctorName));
        }
        if (cost == default)
        {
            throw new ArgumentException("Cost is required", nameof(cost));
        }

        return new DoctorSlot
        {
            Id = Guid.NewGuid(),
            Date = date,
            DoctorId = doctorId,
            DoctorName = doctorName,
            Cost = cost,
            IsReserved = true
        };
    }

    public void Reserve()
    {
        IsReserved = true;
    }
}

using Shared.Domain.Enums;

namespace DoctorAppointmentManagement.Api.Endpoints.ChangeAppointmentStatus.Models;

public class ChangeAppointmentStatusRequestModel
{
    public Guid Id { get; set; }
    public AppointmentStatus Status { get; set; }
}
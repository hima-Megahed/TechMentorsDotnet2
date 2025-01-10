namespace Shared.IntegrationEvents;

public class AppointmentBookedIntegrationEvent
{
    public string PatientName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string DoctorName { get; set; }
}
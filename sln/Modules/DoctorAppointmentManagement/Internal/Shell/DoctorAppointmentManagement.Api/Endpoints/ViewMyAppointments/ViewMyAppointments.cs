using Carter;
using DoctorAppointmentManagement.Api.Endpoints.ChangeAppointmentStatus.Models;
using DoctorAppointmentManagement.Application.Ports.Driving;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace DoctorAppointmentManagement.Api.Endpoints.ViewMyAppointments;

public class ViewMyAppointments : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/view-my-appointments",
            async(IAppointmentManagementService service) =>
            {
                return Results.Ok(await service.GetUpcomingAppointments());
            }).WithTags("Appointment Management");
    }
}
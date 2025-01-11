using Carter;
using DoctorAppointmentManagement.Api.Endpoints.ChangeAppointmentStatus.Models;
using DoctorAppointmentManagement.Application.Ports.Driving;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

namespace DoctorAppointmentManagement.Api.Endpoints.ChangeAppointmentStatus;

public class ChangeAppointmentStatus : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/change-appointment-status",
            async(ChangeAppointmentStatusRequestModel request, IAppointmentManagementService service) =>
            {
                await service.ChangeAppointmentStatus(request.Id, request.Status);
                return Results.Ok();
            }).WithTags("Appointment Management");
    }
}
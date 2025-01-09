using Carter;
using DoctorAvailability.Business.Services.DoctorSlot;
using DoctorAvailability.Business.Services.DoctorSlot.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DoctorAvailability.Presentation.Endpoints.AddSlot;
public class AddSlotEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/AddSlot", async (DoctorSlotRequestModel request, IDoctorSlotService service) =>
        {
            return Results.Ok(await service.AddSlot(request));
        }).WithTags("DoctorAvailability");
    }
}

using AppointmentBooking.Application.GetAvailableSlots.Queries;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AppointmentBooking.Api.Endpoints.GetAvailableSlots;

public class GetAvailableSlots : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/getavailableslots",
            async (IMediator mediator) =>
            {
                return Results.Ok(await mediator.Send(new GetAvailableSlotsQuery()));
            }).WithTags("Patient");
    }
}
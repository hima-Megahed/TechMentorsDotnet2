using AppointmentBooking.Application.BookAppointment.Commands;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace AppointmentBooking.Api.Endpoints.BookAppointment;

public class BookAppointmentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/book-appointment",
            async (BookAppointmentCommand command, IMediator mediator) =>
            {
                await mediator.Send(command);
                return Results.Created();
            }).WithTags("Patient");
    }
}
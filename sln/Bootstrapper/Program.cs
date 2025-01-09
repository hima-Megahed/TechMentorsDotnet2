using AppointmentBooking.Api.Endpoints.GetAvailableSlots;
using AppointmentBooking.Infrastructure.Registrar;
using Carter;
using DoctorAvailability.Presentation.Endpoints.AddSlot;
using DoctorAvailability.Presentation.Registrar;
using Scalar.AspNetCore;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddCarterWithAssemblies(typeof(AddSlotEndpoint).Assembly);
builder.Services
    .AddCarterWithAssemblies(typeof(GetAvailableSlots).Assembly);

// Register module services
builder.Services
    .AddDoctorAvailabilityModule(builder.Configuration);
builder.Services
    .AddAppointmentBookingModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.MapCarter();


// Register modules db initializer
app.UseDoctorAvailabilityDbInitializer();

app.Run();
using AppointmentBooking.Infrastructure.Registrar;
using AppointmentConfirmation.Notification.Registrar;
using Carter;
using DoctorAvailability.Presentation.Registrar;
using Scalar.AspNetCore;
using Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Logging.AddConsole(); // Add Console logging
builder.Logging.AddDebug(); 

builder.Services
    .AddCarterWithAssemblies(DoctorAvailabilityModuleRegistrar.GetModuleAssemblies())
    .AddCarterWithAssemblies(AppointmentBookingModuleRegistrar.GetModuleAssemblies());

var allModulesAssemblies = DoctorAvailabilityModuleRegistrar.GetModuleAssemblies()
    .Concat(AppointmentBookingModuleRegistrar.GetModuleAssemblies())
    .Concat(AppointmentConfirmationModuleRegistrar.GetModuleAssemblies()).ToArray();
builder.Services
    .AddMassTransitWithAssemblies(builder.Configuration, allModulesAssemblies);

// Register module services
DoctorAvailabilityModuleRegistrar
    .AddDoctorAvailabilityModule(builder.Services, builder.Configuration)
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
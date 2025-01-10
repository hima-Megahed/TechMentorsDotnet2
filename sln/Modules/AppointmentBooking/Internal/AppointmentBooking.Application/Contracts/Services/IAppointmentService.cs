﻿using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Application.BookAppointment.DTOs;

namespace AppointmentBooking.Application.Contracts.Services;

public interface IAppointmentService
{
    Task<AppointmentDto> BookAppointment(BookAppointmentCommand bookAppointmentCommand);
}
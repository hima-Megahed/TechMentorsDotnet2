using AppointmentBooking.Application.GetAvailableSlots.DTOs;
using DoctorAvailability.Shared.Facade;
using MediatR;

namespace AppointmentBooking.Application.GetAvailableSlots.Queries;

public class GetAvailableSlotsQuery : IRequest<IList<SlotDto>>
{
}

internal class GetAvailableSlotsQueryHandler(IDoctorAvailabilityFacade doctorAvailabilityFacade)
    : IRequestHandler<GetAvailableSlotsQuery, IList<SlotDto>>
{
    public async Task<IList<SlotDto>> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
    {
        var slots = await doctorAvailabilityFacade.GetDoctorAvailableSlots();
        return SlotDto.From(slots);
    }
}
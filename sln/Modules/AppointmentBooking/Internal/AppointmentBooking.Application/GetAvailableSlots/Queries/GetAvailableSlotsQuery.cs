using AppointmentBooking.Application.GetAvailableSlots.DTOs;
using AppointmentBooking.Shared.Gateways.DoctorAvailability;
using MediatR;

namespace AppointmentBooking.Application.GetAvailableSlots.Queries;

public class GetAvailableSlotsQuery : IRequest<IList<SlotDto>>
{
}

internal class GetAvailableSlotsQueryHandler(IDoctorAvailabilityGateway doctorAvailabilityGateway)
    : IRequestHandler<GetAvailableSlotsQuery, IList<SlotDto>>
{
    public async Task<IList<SlotDto>> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
    {
        var slots = await doctorAvailabilityGateway.GetDoctorAvailableSlots();
        return SlotDto.From(slots);
    }
}
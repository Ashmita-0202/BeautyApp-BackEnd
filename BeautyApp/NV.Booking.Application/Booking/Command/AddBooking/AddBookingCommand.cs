using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TN.Shared.Domain.Abstractions;
using static NV.Booking.Domain.Entities.Bookings;


namespace NV.Booking.Application.Booking.Command.AddBooking
{
    public record AddBookingCommand
    (

        
        string beauticianId,
        string beautyServiceId,
        DateTime appointmentDate,
        ServiceStatus status,
        int totalClients

    ) : IRequest<Result<AddBookingResponse>>;
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Shared.Domain.Abstractions;
using static NV.Booking.Domain.Entities.Bookings;


namespace NV.Booking.Application.Booking.Command.AddBooking
{
    public record AddBookingResponse
    (
        string id,
        string userId,
        string beauticianId,
        string beautyServiceId,
        DateTime appointmentDate,
        ServiceStatus status,
       int totalClients,
        int durationInMinutes
    );
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;


namespace NV.Booking.Application.Booking.Command.UpdateBooking
{
    public record  UpdateBookingCommand
    (      
            string id,
            string userId,
            string beauticianId,
            string beautyServiceId,
            DateTime appointmentDate
            //ServiceStatus status

    ) :IRequest<Result<UpdateBookingResponse>>;
}

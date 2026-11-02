using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Booking.Application.Booking.Command.DeleteBooking
{
    public record DeleteBookingComand(string id):IRequest<Result<bool>>;
}

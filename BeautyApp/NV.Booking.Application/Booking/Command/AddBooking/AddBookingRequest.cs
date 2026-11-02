using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NV.Booking.Domain.Entities.Bookings;

namespace NV.Booking.Application.Booking.Command.AddBooking
{
    public record AddBookingRequest
   (
           
            string beauticianId,
            string beautyServiceId,
            DateTime appointmentDate,
            ServiceStatus status,
            int totalClients
   );
}

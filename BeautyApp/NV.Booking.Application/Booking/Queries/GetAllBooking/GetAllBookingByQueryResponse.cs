using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NV.Booking.Application.Booking.Queries.GetAllBooking
{
    public record GetAllBookingByQueryResponse
  (
            string id,
           string userId,
           string beauticianId,
           string beautyServiceId,
           DateTime appointmentDate,
           //ServiceStatus status,
           int totalClients,
           int durationInMinutes
   );
}

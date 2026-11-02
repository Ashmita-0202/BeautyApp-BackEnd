using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NV.Booking.Application.Booking.Command.UpdateBooking
{
  public record UpdateBookingRequest
  (
            string userId,
            string beauticianId,
            string beautyServiceId,
            DateTime appointmentDate
            //ServiceStatus status
  );
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Booking.Application.Booking.Command.UpdateBooking.RequestCommandMapper
{
   public static class UpdateRequestMapper
    {
        public static UpdateBookingCommand ToCommand(this UpdateBookingRequest request, string id) 
        {
            return new UpdateBookingCommand
                (
                    id,
                    request.userId,
                    request.beauticianId,
                    request.beautyServiceId,
                    request.appointmentDate
                    //request.status
                
                );
        
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Booking.Application.Booking.Command.AddBooking.RequestMapper
{
    public static class AddBookingRequestMapper
    {
        public static AddBookingCommand ToCommand(this AddBookingRequest request)
        {
            return new AddBookingCommand
                 (
                    
                    request.beauticianId,
                    request.beautyServiceId,
                    request.appointmentDate,
                    request.status,
                    request.totalClients

                );
        }
    }
}

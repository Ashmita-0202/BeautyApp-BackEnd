using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Payment.Application.Payment.Command.UpdatePayment.RequestCommandMapper
{
    public static class UdpatePaymentRequestMapper
    {
        public static UpdatePaymentByCommand ToCommand(this UpdatePaymentRequest request, string id)
        {
            return new UpdatePaymentByCommand
                (
                     id,
                  request.bookingId,
                  request.amount,
                  request.paymentMethod,
                  request.status
                
                );
        }

    }
}

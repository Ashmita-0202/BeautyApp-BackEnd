using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Payment.Application.Payment.Command.AddPayment.RequestCommandMapper
{
    public static class AddPaymentRequestMapper
    {
        public static AddPaymentCommand ToCommand(this AddPaymentRequest request) 
        {
            return new AddPaymentCommand
                (
                    request.bookingId,
                    request.amount,
                    request.paymentMethod,
                    request.status
                );
        }
    }
}

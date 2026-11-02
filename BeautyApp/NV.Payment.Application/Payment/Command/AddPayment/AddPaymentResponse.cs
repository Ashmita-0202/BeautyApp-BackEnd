using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NV.Payment.Domain.Entities.Payments;

namespace NV.Payment.Application.Payment.Command.AddPayment
{
    public record  AddPaymentResponse
    (
            string id,
            string bookingId,
            string amount,
            string paymentMethod,
            PaymentStatus status
    );
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;
using static NV.Payment.Domain.Entities.Payments;

namespace NV.Payment.Application.Payment.Command.AddPayment
{
    public record AddPaymentCommand
   (        
          
            string bookingId,
            string amount,
            string paymentMethod,
            PaymentStatus status
    ):IRequest<Result<AddPaymentResponse>>;
}

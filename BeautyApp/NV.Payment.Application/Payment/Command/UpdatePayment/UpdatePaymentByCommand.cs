using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;
using static NV.Payment.Domain.Entities.Payments;

namespace NV.Payment.Application.Payment.Command.UpdatePayment
{
 public record UpdatePaymentByCommand
  (
             string id,
            string bookingId,
            string amount,
            string paymentMethod,
            PaymentStatus status
  ) :IRequest<Result<UpdatePaymentResponse>>;
}

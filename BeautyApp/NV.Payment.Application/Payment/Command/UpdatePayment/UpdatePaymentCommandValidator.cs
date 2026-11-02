using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Payment.Application.Payment.Command.UpdatePayment
{
    public class UpdatePaymentCommandValidator:AbstractValidator<UpdatePaymentByCommand>
    {
        public UpdatePaymentCommandValidator()
        {
          RuleFor(x => x.bookingId).NotEmpty().WithMessage("bookingId is required");
        
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Payment.Application.Payment.Command.AddPayment
{
    public  class AddPaymentCommandValidator:AbstractValidator<AddPaymentCommand>
    {
        public AddPaymentCommandValidator()
        { 
            RuleFor(x=>x.bookingId).NotEmpty().WithMessage("bookingId is required");
        }
    }
}

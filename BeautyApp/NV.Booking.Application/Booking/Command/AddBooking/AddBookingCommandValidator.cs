using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Booking.Application.Booking.Command.AddBooking
{
    public class AddBookingCommandValidator : AbstractValidator<AddBookingCommand>
    {
        public AddBookingCommandValidator()
        {
            RuleFor(x => x.beauticianId)
                       .NotEmpty()
                       .WithMessage("beauticianId is required.");
        }
    }
}

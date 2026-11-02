using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians
{
    public class AddServicesToBeauticianCommandValidator:AbstractValidator<AddServicesToBeauticianCommand>
    {
        public AddServicesToBeauticianCommandValidator()
        {
            RuleFor(x => x.beauticianId)
                        .NotEmpty()
                        .WithMessage("beauticianId is required.");
        }
    }
}

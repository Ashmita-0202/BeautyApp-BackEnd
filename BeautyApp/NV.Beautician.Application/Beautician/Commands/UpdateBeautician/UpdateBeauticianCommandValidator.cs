using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautician
{
    public class UpdateBeauticianCommandValidator:AbstractValidator<UpdateBeauticianCommand>
    {
        public UpdateBeauticianCommandValidator()
        {
            RuleFor(x => x.userId)
                        .NotEmpty()
                        .WithMessage("userId is required.");
        }
    }
}

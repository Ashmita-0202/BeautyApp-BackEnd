using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautyServices
{
    public class AddBeautyServicesCommandValidator:AbstractValidator<AddBeautyServicesCommand>
    {
        public AddBeautyServicesCommandValidator()
        {
            RuleFor(x=>x.name).NotEmpty().WithMessage("name is required");
        }
    }
}

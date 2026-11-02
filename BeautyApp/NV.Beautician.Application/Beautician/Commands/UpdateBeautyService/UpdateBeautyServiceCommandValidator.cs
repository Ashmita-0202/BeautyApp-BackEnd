using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautyService
{
    public  class UpdateBeautyServiceCommandValidator:AbstractValidator<UpdateBeautyServiceCommand>
    {
        public UpdateBeautyServiceCommandValidator()
        {
           RuleFor(x => x.name).NotEmpty().WithMessage("name is required");
        }
    }
}

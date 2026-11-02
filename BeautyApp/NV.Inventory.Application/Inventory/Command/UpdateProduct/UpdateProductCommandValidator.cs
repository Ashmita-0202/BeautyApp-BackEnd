using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Inventory.Application.Inventory.Command.UpdateProduct
{
    public  class UpdateProductCommandValidator:AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage(" is required");



        }
    }
}

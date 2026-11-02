using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Inventory.Application.Inventory.Command.AddProduct
{
    public class AddProductCommandValidator:AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator() 
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("name is required");
            RuleFor(x => x.quantity).NotEmpty().WithMessage("quantity is required");
        }
    }
}

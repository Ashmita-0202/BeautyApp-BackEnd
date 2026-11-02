using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace NV.Feedbacks.Application.Command.AddReview
{
   public class AddReviewCommandValidator:AbstractValidator<AddReviewCommand>
    {
        public AddReviewCommandValidator() 
        {
            RuleFor(x => x.beauticianId).NotEmpty().WithMessage("beauticianId is required");

        }
    }
}

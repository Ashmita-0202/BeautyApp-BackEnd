using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace NV.Feedbacks.Application.Feedbacks.Command.UpdateReview
{
   public class UpdateReviewCommandValidator:AbstractValidator<UpdateReviewCommand>
    {
        public UpdateReviewCommandValidator() 
        {
            RuleFor(x => x.userId).NotEmpty().WithMessage("user id is required");
        
        }
    }
}

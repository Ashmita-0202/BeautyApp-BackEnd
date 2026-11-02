using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Command.UpdateReview
{
  public record UpdateReviewCommand
   (        
            string id,
            string userId,
            string beauticianId,
            double rating,
            string comments
      
  ) :IRequest<Result<UpdateReviewResponse>>;
}

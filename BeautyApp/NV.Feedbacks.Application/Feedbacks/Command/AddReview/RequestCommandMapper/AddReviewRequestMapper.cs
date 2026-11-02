using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Feedbacks.Application.Command.AddReview.RequestCommandMapper
{
   public static class AddReviewRequestMapper
    {
        public static AddReviewCommand ToCommand(this AddReviewRequest request) 
        {
            return new AddReviewCommand
                (
                     request.beauticianId,
                    request.rating,
                    request.comments
                  
                );
        }
    }
}

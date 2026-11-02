using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Feedbacks.Application.Feedbacks.Command.UpdateReview.RequestCommandMapper
{
   public static class UpdateReviewRequestMapper
    {
        public static UpdateReviewCommand ToCommand(this UpdateReviewRequest request, string id) 
        {

            return new UpdateReviewCommand
                (
                    id,
                    request.userId,
                    request.beauticianId,
                    request.rating,
                    request.comments

                );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Feedbacks.Application.Feedbacks.Command.UpdateReview
{
   public record UpdateReviewResponse
    (
            string id,
            string userId,
            string beauticianId,
            double rating,
            string comments
   );
}

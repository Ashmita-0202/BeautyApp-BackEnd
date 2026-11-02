using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Feedbacks.Application.Command.AddReview
{
  public record AddReviewRequest
    (
            string beauticianId,
            double rating,
            string comments
           
            
      
  );
}

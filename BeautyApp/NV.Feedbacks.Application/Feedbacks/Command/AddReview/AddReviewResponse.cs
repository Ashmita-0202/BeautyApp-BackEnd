using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Feedbacks.Application.Command.AddReview
{
    public record AddReviewResponse
    (
             string id="",
            string userId = "",
            string beauticianId = "",
            double rating= 0,
            string comments = ""
          
           
    );

    
}

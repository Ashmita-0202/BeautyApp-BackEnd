using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Feedbacks.Application.Feedbacks.Queries.GetReviewById
{
    public record  GetReviewByIdQueryResponse
    (
            string id,
            string userId,
            string beauticianId,
            int rating,
            string comments
    );
}

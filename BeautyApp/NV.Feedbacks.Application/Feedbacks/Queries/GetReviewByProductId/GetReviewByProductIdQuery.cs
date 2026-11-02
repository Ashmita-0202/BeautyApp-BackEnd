using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByProductId
{
    public record  GetReviewByProductIdQuery
    (string productId):IRequest<Result<List<GetReviewByProductIdQueryResponse>>>;
}

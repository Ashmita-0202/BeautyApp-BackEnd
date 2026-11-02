using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Queries.GetReviewById
{
    public record  GetReviewByIdQuery
    (string id):IRequest<Result<GetReviewByIdQueryResponse>>;
}

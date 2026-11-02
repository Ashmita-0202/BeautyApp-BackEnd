using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Command.AddReview
{
    public record  AddReviewCommand
    (
            string beauticianId,
            double rating,
            string comments

    ) :IRequest<Result<AddReviewResponse>>;
}

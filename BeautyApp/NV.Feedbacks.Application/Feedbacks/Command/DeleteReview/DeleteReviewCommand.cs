using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Command.DeleteReview
{
    public record DeleteReviewCommand
   (string id):IRequest<Result<bool>>;
}

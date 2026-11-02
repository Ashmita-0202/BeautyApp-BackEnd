using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Feedbacks.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Command.DeleteReview
{
    public class DeleteReviewCommandHandler:IRequestHandler<DeleteReviewCommand,Result<bool>>
    {
        private readonly IReviewServices _reviewServices;
        private readonly IMapper _mapper;

        public DeleteReviewCommandHandler(IReviewServices reviewServices, IMapper mapper) 
        {
            _reviewServices=reviewServices;
            _mapper=mapper;
        
        }

        public async Task<Result<bool>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteReview = await _reviewServices.Delete(request.id, cancellationToken);
                if (deleteReview is null)
                {
                    return Result<bool>.Failure("NotFound", "review not Found");
                }
                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting {request.id}", ex);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NV.Feedbacks.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Command.UpdateReview
{
    public  class UpdateReviewCommandHandler:IRequestHandler<UpdateReviewCommand,Result<UpdateReviewResponse>>
    {
        private readonly IReviewServices _reviewServices;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateReviewCommand> _validator;

        public UpdateReviewCommandHandler(IReviewServices reviewServices,IMapper mapper,IValidator<UpdateReviewCommand> validator) 
        {
            _reviewServices=reviewServices;
            _mapper=mapper;
            _validator=validator;
        }

        public async Task<Result<UpdateReviewResponse>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {

            try
            {

                var validationAction = await _validator.ValidateAsync(request, cancellationToken);
                if ((!validationAction.IsValid))
                {
                    var errors = string.Join(", ", validationAction.Errors.Select(x => x.ErrorMessage));
                    return Result<UpdateReviewResponse>.Failure(errors);

                }

                var updateReview = await _reviewServices.Update(request.id, request);

                if (updateReview.Errors.Any())
                {
                    var errors = string.Join(", ", updateReview.Errors);
                    return Result<UpdateReviewResponse>.Failure(errors);
                }

                if (updateReview is null || !updateReview.IsSuccess)
                {
                    return Result<UpdateReviewResponse>.Failure("Updates Review failed");
                }

                var reviewDisplay = _mapper.Map<UpdateReviewResponse>(updateReview.Data);
                return Result<UpdateReviewResponse>.Success(reviewDisplay);
            }
            catch (Exception ex)
            {
                throw new Exception("$\"An error occurred while updating Review by {request.id}", ex);
            }
        }
    }
}

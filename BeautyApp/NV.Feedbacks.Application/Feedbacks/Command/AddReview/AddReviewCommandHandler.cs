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

namespace NV.Feedbacks.Application.Command.AddReview
{
   public class AddReviewCommandHandler:IRequestHandler<AddReviewCommand,Result<AddReviewResponse>>
    {
        private IReviewServices _reviewServices;
        private readonly IMapper _mapper;
        private readonly IValidator<AddReviewCommand> _validator;

        public AddReviewCommandHandler(IReviewServices reviewServices, IMapper mapper,IValidator<AddReviewCommand> validator)
        {
            _reviewServices=reviewServices;
            _mapper=mapper;
            _validator=validator;
        }

        public async Task<Result<AddReviewResponse>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                    return Result<AddReviewResponse>.Failure(errors);
                }

                var addReview = await _reviewServices.AddReview(request);

                if (addReview.Errors.Any())
                {
                    var errors = string.Join(", ", addReview.Errors);
                    return Result<AddReviewResponse>.Failure(errors);
                }

                if (addReview is null || !addReview.IsSuccess)
                {
                    return Result<AddReviewResponse>.Failure(" ");
                }

                var reviewDisplay = _mapper.Map<AddReviewResponse>(addReview.Data);
                return Result<AddReviewResponse>.Success(reviewDisplay);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding review", ex);

            }
        }
    }
}

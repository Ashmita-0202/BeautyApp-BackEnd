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
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Feedbacks.Application.Feedbacks.Queries.AllReview
{
   public  class GetAllReviewByQueryHandler:IRequestHandler<GetAllReviewByQuery,Result<PagedResult<GetAllReviewByQueryResponse>>>
    {
        private readonly IReviewServices _reviewServices;
        private readonly IMapper _mapper;

        public GetAllReviewByQueryHandler(IReviewServices reviewServices,IMapper mapper) 
        { 
            _reviewServices=reviewServices;
            _mapper=mapper;
        }

        public async Task<Result<PagedResult<GetAllReviewByQueryResponse>>> Handle(GetAllReviewByQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var review = await _reviewServices.GetAllReview(request.PaginationRequest);

                var reviewResponse = _mapper.Map<PagedResult<GetAllReviewByQueryResponse>>(review.Data);

                return Result<PagedResult<GetAllReviewByQueryResponse>>.Success(reviewResponse);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<GetAllReviewByQueryResponse>>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}

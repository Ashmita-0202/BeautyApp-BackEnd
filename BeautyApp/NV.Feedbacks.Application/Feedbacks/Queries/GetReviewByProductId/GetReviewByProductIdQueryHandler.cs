using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByBeauticianId;
using NV.Feedbacks.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByProductId
{
    public  class GetReviewByProductIdQueryHandler:IRequestHandler<GetReviewByProductIdQuery, Result<List<GetReviewByProductIdQueryResponse>>>
    {
        private readonly IReviewServices _reviewServices;
        private readonly IMapper _mapper;

        public GetReviewByProductIdQueryHandler(IReviewServices reviewServices,IMapper mapper) 
        {
            _reviewServices=reviewServices;
            _mapper=mapper;
        
        }

        public async Task<Result<List<GetReviewByProductIdQueryResponse>>> Handle(GetReviewByProductIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reviewByProductId = await _reviewServices.GetReviewByProductId(request.productId, cancellationToken);
                return Result<List<GetReviewByProductIdQueryResponse>>.Success(reviewByProductId.Data);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching a review by Product id", ex);

            }
        }
    }
}

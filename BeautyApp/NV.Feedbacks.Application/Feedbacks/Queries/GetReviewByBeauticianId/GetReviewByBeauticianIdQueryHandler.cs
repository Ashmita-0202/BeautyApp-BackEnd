using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Feedbacks.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByBeauticianId
{
    public class GetReviewByBeauticianIdQueryHandler:IRequestHandler<GetReviewByBeauticianIdQuery,Result<List<GetReviewByBeauticianIdQueryResponse>>>
    {
        private readonly IReviewServices _reviewServices;
        private readonly IMapper _mapper;

        public GetReviewByBeauticianIdQueryHandler(IReviewServices reviewServices,IMapper mapper)
        {
            _reviewServices=reviewServices;
            _mapper=mapper;
            
        }

        public async Task<Result<List<GetReviewByBeauticianIdQueryResponse>>> Handle(GetReviewByBeauticianIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reviewByBeauticianId = await _reviewServices.GetReviewByBeauticianId(request.beauticianId, cancellationToken);
                return Result<List<GetReviewByBeauticianIdQueryResponse>>.Success(reviewByBeauticianId.Data);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching a review by Beautician id", ex);

            }
        }
    }
}

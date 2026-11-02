using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NV.Feedbacks.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.IRepository;

namespace NV.Feedbacks.Application.Feedbacks.Queries.GetReviewById
{
    public  class GetReviewByIdQueryHandler:IRequestHandler<GetReviewByIdQuery,Result<GetReviewByIdQueryResponse>>
    {
        private readonly IReviewServices _reviewServices;
        private readonly IUnitOfWork _unitOfWork;

        public GetReviewByIdQueryHandler(IReviewServices reviewServices,IUnitOfWork unitOfWork)
        {
            _reviewServices=reviewServices;
            _unitOfWork=unitOfWork;
        
        }

        public async Task<Result<GetReviewByIdQueryResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var reviewById = await _reviewServices.GetReviewById(request.id);

                return Result<GetReviewByIdQueryResponse>.Success(reviewById.Data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Review by Id", ex);

            }
        }
    }
}

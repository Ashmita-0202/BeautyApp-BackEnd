using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NV.Feedbacks.Application.Command.AddReview;
using NV.Feedbacks.Application.Feedbacks.Command.UpdateReview;
using NV.Feedbacks.Application.Feedbacks.Queries.AllReview;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByBeauticianId;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewById;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByProductId;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Feedbacks.Application.ServiceInterface
{
    public interface IReviewServices
    {
        Task<Result<AddReviewResponse>> AddReview(AddReviewCommand addReviewCommand);
        Task<Result<PagedResult<GetAllReviewByQueryResponse>>>GetAllReview(PaginationRequest paginationRequest,CancellationToken cancellationToken=default);

        Task<Result<GetReviewByIdQueryResponse>> GetReviewById(string id, CancellationToken cancellationToken = default);
        Task<Result<List<GetReviewByBeauticianIdQueryResponse>>> GetReviewByBeauticianId(string beauticianId, CancellationToken cancellationToken=default);
        Task<Result<List<GetReviewByProductIdQueryResponse>>> GetReviewByProductId(string productId,CancellationToken cancellationToken=default);
        Task<Result<UpdateReviewResponse>> Update(string id, UpdateReviewCommand updateReviewCommand);
        Task<Result<bool>> Delete(string id, CancellationToken cancellationToken);


    }
}

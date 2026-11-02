using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NV.Feedbacks.Application.Command.AddReview;
using NV.Feedbacks.Application.Feedbacks.Command.UpdateReview;
using NV.Feedbacks.Application.Feedbacks.Queries.AllReview;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByBeauticianId;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewById;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByProductId;
using NV.Feedbacks.Application.ServiceInterface;
using NV.Feedbacks.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;


namespace NV.Feedbacks.Infrastructure.ServiceImpl
{
    public class ReviewServices : IReviewServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public ReviewServices(IUnitOfWork unitOfWork,IMapper mapper, ITokenService tokenService)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
            _tokenService=tokenService;
        }

        public async Task<Result<AddReviewResponse>> AddReview(AddReviewCommand addReviewCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    string userId = _tokenService.GetUserId();
                    string newId = Guid.NewGuid().ToString();
                    var reviewData = new Review
                    (
                        newId,
                        userId,
                        addReviewCommand.beauticianId,
                        addReviewCommand.rating,
                        addReviewCommand.comments,
                       " "
                    );

                    await _unitOfWork.BaseRepository<Review>().AddAsync(reviewData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTO = _mapper.Map<AddReviewResponse>(reviewData);
                    return Result<AddReviewResponse>.Success(resultDTO);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding the review.", ex);
                }
            }
        }

        public async Task<Result<bool>> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var review = await _unitOfWork.BaseRepository<Review>().GetByGuIdAsync(id);
                if (review is null)
                {
                    return Result<bool>.Failure("NotFound", "review Cannot be Found");
                }

                _unitOfWork.BaseRepository<Review>().Delete(review);
                await _unitOfWork.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting review having {id}", ex);
            }
        }

        public async Task<Result<PagedResult<GetAllReviewByQueryResponse>>> GetAllReview(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var reviews = await _unitOfWork.BaseRepository<Review>().GetAllAsyncWithPagination();

                var reviwPagedResult = await reviews.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);


                var allReviewResponse = _mapper.Map<PagedResult<GetAllReviewByQueryResponse>>(reviwPagedResult.Data);
                return Result<PagedResult<GetAllReviewByQueryResponse>>.Success(allReviewResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all Review ", ex);
            }
        }

        public async Task<Result<List<GetReviewByBeauticianIdQueryResponse>>> GetReviewByBeauticianId(string beauticianId, CancellationToken cancellationToken = default)
        {
            try
            {
                var review = await _unitOfWork.BaseRepository<Review>().GetConditionalAsync(x => x.BeauticianId == beauticianId);
                if (review is null)
                {
                    return Result<List<GetReviewByBeauticianIdQueryResponse>>.Failure("NotFound", "Review Data are not found");
                }

                var reviewResponse = _mapper.Map<List<GetReviewByBeauticianIdQueryResponse>>(review);

                return Result<List<GetReviewByBeauticianIdQueryResponse>>.Success(reviewResponse);


            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching Review By Beautician Id", ex);

            }
        }

        public async Task<Result<GetReviewByIdQueryResponse>> GetReviewById(string id, CancellationToken cancellationToken = default)
        {
            try
            {

                var review = await _unitOfWork.BaseRepository<Review>().GetByGuIdAsync(id);
                var reviewResponse = _mapper.Map<GetReviewByIdQueryResponse>(review);
                return Result<GetReviewByIdQueryResponse>.Success(reviewResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching review by using Id", ex);
            }
        }

        public async Task<Result<List<GetReviewByProductIdQueryResponse>>> GetReviewByProductId(string productId, CancellationToken cancellationToken = default)
        {

            try
            {
                var review = await _unitOfWork.BaseRepository<Review>().GetConditionalAsync(x => x.ProductsId == productId);
                if (review is null)
                {
                    return Result<List<GetReviewByProductIdQueryResponse>>.Failure("NotFound", "Review Data are not found");
                }

                var reviewResponse = _mapper.Map<List<GetReviewByProductIdQueryResponse>>(review);

                return Result<List<GetReviewByProductIdQueryResponse>>.Success(reviewResponse);


            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while fetching Review By Product Id", ex);

            }
        }

        public async Task<Result<UpdateReviewResponse>> Update(string id, UpdateReviewCommand updateReviewCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (id == null)
                    {
                        return Result<UpdateReviewResponse>.Failure("NotFound", "Please provide valid Review Id");
                    }

                    var reviewToBeUpdated = await _unitOfWork.BaseRepository<Review>().GetByGuIdAsync(id);
                    if (reviewToBeUpdated is null)
                    {
                        return Result<UpdateReviewResponse>.Failure("NotFound", "Review are not Found");
                    }

                    _mapper.Map(updateReviewCommand, reviewToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateReviewResponse
                        (

                              id,
                             reviewToBeUpdated.UserId,
                             reviewToBeUpdated.BeauticianId,
                             reviewToBeUpdated.Rating,
                             reviewToBeUpdated.Comments
                        );

                    return Result<UpdateReviewResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the review", ex);
                }

            }

        }
    }
}

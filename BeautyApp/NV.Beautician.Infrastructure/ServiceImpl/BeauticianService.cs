
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.Beautician.Queries.GetBeauticianById;
using NV.Beautician.Application.ServiceInterface;
using NV.Buautician.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;

namespace NV.Beautician.Infrastructure.ServiceImpl
{
    public class BeauticianService:IBeauticianService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BeauticianService(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task<Result<AddBeauticianResponse>> Add(AddBeauticianCommand addBeauticianCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    
                    string newId = Guid.NewGuid().ToString();
                    var beauticiansData = new Beauticians
                    (
                            newId,
                            addBeauticianCommand.experienceYear,
                            addBeauticianCommand.skills,
                            addBeauticianCommand.userId
                    );

                    await _unitOfWork.BaseRepository<Beauticians>().AddAsync(beauticiansData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddBeauticianResponse>(beauticiansData);
                    return Result<AddBeauticianResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding Beauticians ", ex);

                }
            }

        }

        

        public async Task<Result<bool>> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var beauticians = await _unitOfWork.BaseRepository<Beauticians>().GetByGuIdAsync(id);
                if (beauticians is null)
                {
                    return Result<bool>.Failure("NotFound", "Beauticians Cannot be Found");
                }

                _unitOfWork.BaseRepository<Beauticians>().Delete(beauticians);
                await _unitOfWork.SaveChangesAsync();


                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting Beauticians having {id}", ex);
            }


        }

        public async Task<Result<PagedResult<GetAllBeauticianByQueryResponse>>> GetAllBeautician(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var beautician = await _unitOfWork.BaseRepository<Beauticians>().GetAllAsyncWithPagination();

                var beauticianPagedResult = await beautician.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);


                var allBeauticianResponse = _mapper.Map<PagedResult<GetAllBeauticianByQueryResponse>>(beauticianPagedResult.Data);
                return Result<PagedResult<GetAllBeauticianByQueryResponse>>.Success(allBeauticianResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all Beautician ", ex);
            }
        }

        public async Task<Result<GetBeauticianByIdQueryResponse>> GetBeauticiansById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
               
                var beauticians = await _unitOfWork.BaseRepository<Beauticians>().GetByGuIdAsync(id);
                var beauticiansResponse = _mapper.Map<GetBeauticianByIdQueryResponse>(beauticians);      
                return Result<GetBeauticianByIdQueryResponse>.Success(beauticiansResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching beauticians by using Id", ex);
            }

        }

        public async Task<Result<UpdateBeauticianResponse>> Update(string id, UpdateBeauticianCommand updateBeauticianCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (id == null)
                    {
                        return Result<UpdateBeauticianResponse>.Failure("NotFound", "Please provide valid beautician Id");
                    }

                    var beauticianToBeUpdated = await _unitOfWork.BaseRepository<Beauticians>().GetByGuIdAsync(id);
                    if (beauticianToBeUpdated is null)
                    {
                        return Result<UpdateBeauticianResponse>.Failure("NotFound", "Beauticians are not Found");
                    }
                 
                    _mapper.Map(updateBeauticianCommand, beauticianToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateBeauticianResponse
                        (

                          id,
                          beauticianToBeUpdated.ExperienceYear,
                          beauticianToBeUpdated.Skills,
                          beauticianToBeUpdated.UserId
                        );

                    return Result<UpdateBeauticianResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the Beauticians", ex);
                }

            }

        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NV.Beautician.Application.Beautician.Commands.AddBeautyServices;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautyService;
using NV.Beautician.Application.Beautician.Queries.AllBeautyServices;
using NV.Beautician.Application.Beautician.Queries.BeautyServiceById;
using NV.Beautician.Application.ServiceInterface;
using NV.Buautician.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;

namespace NV.Beautician.Infrastructure.ServiceImpl
{
    public class BeautyService : IBeautyServices
    {
        private readonly IBeautyServices _beautyServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BeautyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
        public async Task<Result<AddBeautyServicesResponse>> Add(AddBeautyServicesCommand addBeautyServicesCommand)
        {
        
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    string newId = Guid.NewGuid().ToString();
                    var beautyServicesData = new BeautyServices
                    (
                            newId,
                            addBeautyServicesCommand.name,
                            addBeautyServicesCommand.description,
                            addBeautyServicesCommand.price,
                            addBeautyServicesCommand.durationInMinutes

                    );

                    await _unitOfWork.BaseRepository<BeautyServices>().AddAsync((BeautyServices)beautyServicesData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddBeautyServicesResponse>(beautyServicesData);
                    return Result<AddBeautyServicesResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding Beauty Services ", ex);

                }
            }

        }

        public async Task<Result<bool>> Delete(string id, CancellationToken cancellationToken)
        {

            try
            {
                var beautyService = await _unitOfWork.BaseRepository<BeautyServices>().GetByGuIdAsync(id);
                if (beautyService is null)
                {
                    return Result<bool>.Failure("NotFound", "beauty service Cannot be Found");
                }

                _unitOfWork.BaseRepository<BeautyServices>().Delete(beautyService);
                await _unitOfWork.SaveChangesAsync();


                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting beauty Services having {id}", ex);
            }

        }

        public async Task<Result<PagedResult<GetAllBeautyServiceByQueryResponse>>> GetAllBeautyService(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var beautyService = await _unitOfWork.BaseRepository<BeautyServices>().GetAllAsyncWithPagination();

                var beautyServicePdagedResult = await beautyService.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);


                var allBeautyServiceResponse = _mapper.Map<PagedResult<GetAllBeautyServiceByQueryResponse>>(beautyServicePdagedResult.Data);
                return Result<PagedResult<GetAllBeautyServiceByQueryResponse>>.Success(allBeautyServiceResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all Beauty service ", ex);
            }
        }

        public async Task<Result<GetBeautyServiceByIdQueryResponse>> GetBeautyServiceById(string id, CancellationToken cancellationToke = default)
        {
            try
            {

                var beautyServices = await _unitOfWork.BaseRepository<BeautyServices>().GetByGuIdAsync(id);
                var beautyServiceResponse = _mapper.Map<GetBeautyServiceByIdQueryResponse>(beautyServices);
                return Result<GetBeautyServiceByIdQueryResponse>.Success(beautyServiceResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching beautyServices by using Id", ex);
            }
        }

        public async Task<Result<UpdateBeautyServiceResponse>> Update(string id, UpdateBeautyServiceCommand updateBeautyServiceCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (id == null)
                    {
                        return Result<UpdateBeautyServiceResponse>.Failure("NotFound", "Please provide valid beautyService Id");
                    }

                    var beautyServiceToBeUpdated = await _unitOfWork.BaseRepository<BeautyServices>().GetByGuIdAsync(id);
                    if (beautyServiceToBeUpdated is null)
                    {
                        return Result<UpdateBeautyServiceResponse>.Failure("NotFound", "BeautyServices are not Found");
                    }

                    _mapper.Map(updateBeautyServiceCommand, beautyServiceToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateBeautyServiceResponse
                        (

                          id,
                          beautyServiceToBeUpdated.Name,
                          beautyServiceToBeUpdated.Description,
                          beautyServiceToBeUpdated.Price,
                          beautyServiceToBeUpdated.DurationInMinutes

                        );

                    return Result<UpdateBeautyServiceResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the Beauty Service", ex);
                }

            }
        }
    }
}


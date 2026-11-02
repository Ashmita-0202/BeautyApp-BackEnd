using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TN.Authentication.Domain.Entities;
using TN.Setup.Application.ServiceInterface;
using TN.Setup.Application.Setup.Command.AddCompany;
using TN.Setup.Application.Setup.Command.UpdateCompany;
using TN.Setup.Application.Setup.Queries.Company;
using TN.Setup.Application.Setup.Queries.CompanyById;

using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;
using TN.Shared.Domain.Static.Cache;

namespace TN.Setup.Infrastructure.ServiceImpl
{
    public class CompanyServices : ICompanyServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCacheRepository _memoryCacheRepository;
        private readonly ITokenService _tokenService;

        public CompanyServices(IUnitOfWork unitOfWork,IMapper mapper,ITokenService tokenService, IMemoryCacheRepository memoryCacheRepository ) 
        { 
            _memoryCacheRepository = memoryCacheRepository;
            _unitOfWork=unitOfWork;
            _mapper=mapper;
            _tokenService=tokenService;
        }

        public async Task<Result<AddCompanyResponse>> Add(AddCompanyCommand addCompanyCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    string userId = _tokenService.GetUserId();
                    string newId = Guid.NewGuid().ToString();
                    var companyData = new Company
                    (
                         newId,
                          addCompanyCommand.name,
                          addCompanyCommand.address,
                          addCompanyCommand.email,
                          addCompanyCommand.contactNumber,
                          addCompanyCommand.pan,
                          addCompanyCommand.imageUrl,
                          addCompanyCommand.isEnabled,
                          DateTime.UtcNow,
                          userId,
                          default,
                          "",
                          addCompanyCommand.isDeleted,
                          addCompanyCommand.organizationId

                     );

                    await _unitOfWork.BaseRepository<Company>().AddAsync(companyData);

                    var companySettings = new CompanySettings
                        (
                        Guid.NewGuid().ToString(),
                        true,
                        true,
                        newId

                        );

                    await _unitOfWork.BaseRepository<CompanySettings>().AddAsync(companySettings);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddCompanyResponse>(companyData);
                    return Result<AddCompanyResponse>.Success(resultDTOs);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding Company", ex);

                }
            }


        }

        public async Task<Result<bool>> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                
                await _memoryCacheRepository.RemoveAsync(CacheKeys.Company);

                var company = await _unitOfWork.BaseRepository<Company>().GetByGuIdAsync(id);
                if (company is null)
                {
                    return Result<bool>.Failure("NotFound", "Company Cannot be Found");
                }

                _unitOfWork.BaseRepository<Company>().Delete(company);
                await _unitOfWork.SaveChangesAsync();


                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting company having {id}", ex);
            }

        }

        public async Task<Result<PagedResult<GetAllCompanyQueryResponse>>> GetAllCompany(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {

                var company=await _unitOfWork.BaseRepository<Company>().GetAllAsyncWithPagination();
                var  companyPagedResult= await company.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);
                var allCompanyResponse= _mapper.Map<PagedResult<GetAllCompanyQueryResponse>>(companyPagedResult.Data);

                return Result<PagedResult<GetAllCompanyQueryResponse>>.Success(allCompanyResponse);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all company", ex);
            
            }
        }

        public async Task<Result<GetCompanyByIdResponse>> GetCompanyById(string companyId, CancellationToken cancellationToken = default)
        {
            try
            {
                var company = await _unitOfWork.BaseRepository<Company>().GetByGuIdAsync(companyId);
                var companyResponse = _mapper.Map<GetCompanyByIdResponse>(company);
                return Result<GetCompanyByIdResponse>.Success(companyResponse);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching company by id", ex);
            
            }
        }

        public async Task<Result<UpdateCompanyResponse>> Update(string companyId, UpdateCompanyCommand updateCompanyCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var userId = _tokenService.GetUserId();
                    if (companyId == null)
                    {
                        return Result<UpdateCompanyResponse>.Failure("NotFound", "Please provide valid organizationId");
                    }

                    var companyToBeUpdated = await _unitOfWork.BaseRepository<Company>().GetByGuIdAsync(companyId);
                    if (companyToBeUpdated is null)
                    {
                        return Result<UpdateCompanyResponse>.Failure("NotFound", "LedgerGroup are not Found");
                    }

                    companyToBeUpdated.ModifiedBy = userId;
                    companyToBeUpdated.ModifiedDate = DateTime.UtcNow;
                    _mapper.Map(companyToBeUpdated, companyToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateCompanyResponse
                        (
                            companyId,
                            companyToBeUpdated.Name,
                            companyToBeUpdated.Address,
                            companyToBeUpdated.Email,
                            companyToBeUpdated.ContactNumber,
                            companyToBeUpdated.PAN,
                            companyToBeUpdated.ImageUrl,
                            companyToBeUpdated.IsEnabled,
                            companyToBeUpdated.CreatedDate,
                            companyToBeUpdated.CreatedBy,
                            companyToBeUpdated.ModifiedDate,
                            companyToBeUpdated.ModifiedBy,
                            companyToBeUpdated.IsDeleted

                        );

                    return Result<UpdateCompanyResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {

                    throw new Exception($"An error occured while updating organization", ex);
                }
            }

        }

    }
}

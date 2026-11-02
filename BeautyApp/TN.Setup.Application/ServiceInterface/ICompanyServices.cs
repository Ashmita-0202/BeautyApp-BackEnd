
using TN.Setup.Application.Setup.Command.AddCompany;
using TN.Setup.Application.Setup.Command.UpdateCompany;
using TN.Setup.Application.Setup.Queries.Company;
using TN.Setup.Application.Setup.Queries.CompanyById;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;


namespace TN.Setup.Application.ServiceInterface
{
    public interface ICompanyServices
    {
        Task<Result<PagedResult<GetAllCompanyQueryResponse>>> GetAllCompany(PaginationRequest paginationRequest, CancellationToken cancellationToken = default);
        Task<Result<AddCompanyResponse>> Add(AddCompanyCommand addCompanyCommand);
        Task<Result<UpdateCompanyResponse>> Update(string companyId, UpdateCompanyCommand updateCompanyCommand);
        Task<Result<GetCompanyByIdResponse>> GetCompanyById(string companyId, CancellationToken cancellationToken = default);
       
        Task<Result<bool>> Delete(string id, CancellationToken cancellationToken);
    }
}

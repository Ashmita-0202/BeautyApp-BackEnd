
using TN.Authentication.Application.Authentication.Queries.GetExpiredDateItemStatusByCompany;
using TN.Shared.Application.Shared.Command.UpdateItemStatusByCompany;
using TN.Shared.Domain.Abstractions;

namespace TN.Shared.Application.ServiceInterface
{
    public interface ISettingServices
    {
        Task<Result<GetItemStatusByCompanyResponse>> GetItemStatusByCompany(string companyId,CancellationToken cancellationToken);
        Task<Result<UpdateItemStatusByCompanyResponse>> UpdateItemStatusByCompany(string companyId, bool isExpiredDate, bool isSerialNo, CancellationToken cancellationToken);
    }
}

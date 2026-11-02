using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NV.Beautician.Application.Beautician.Commands.AddBeautyServices;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautyService;
using NV.Beautician.Application.Beautician.Queries.AllBeautyServices;
using NV.Beautician.Application.Beautician.Queries.BeautyServiceById;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.ServiceInterface
{
   public interface IBeautyServices
    {
        Task<Result<AddBeautyServicesResponse>> Add(AddBeautyServicesCommand addBeautyServicesCommand);
   
        Task<Result<PagedResult<GetAllBeautyServiceByQueryResponse>>> GetAllBeautyService(PaginationRequest paginationRequest, CancellationToken cancellationToken=default);
    
        Task<Result<GetBeautyServiceByIdQueryResponse>> GetBeautyServiceById(string id, CancellationToken cancellationToke=default);
        Task<Result<UpdateBeautyServiceResponse>> Update(string id, UpdateBeautyServiceCommand updateBeautyServiceCommand);
    Task<Result<bool>> Delete(string id, CancellationToken cancellationToken);
    }
}

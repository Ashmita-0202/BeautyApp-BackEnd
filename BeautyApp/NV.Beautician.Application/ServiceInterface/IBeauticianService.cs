using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.AddBeautyServices;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.Beautician.Queries.GetBeauticianById;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.ServiceInterface
{
    public  interface IBeauticianService
    {
        Task<Result<PagedResult<GetAllBeauticianByQueryResponse>>> GetAllBeautician(PaginationRequest paginationRequest, CancellationToken cancellationToken = default);
        Task<Result<GetBeauticianByIdQueryResponse>> GetBeauticiansById(string id,CancellationToken cancellationToken = default);
        Task<Result<AddBeauticianResponse>> Add(AddBeauticianCommand addBeauticianCommand);
        Task<Result<UpdateBeauticianResponse>> Update(string id,UpdateBeauticianCommand updateBeauticianCommand);
        Task<Result<bool>> Delete(string id, CancellationToken cancellationToken);
       
    }
}

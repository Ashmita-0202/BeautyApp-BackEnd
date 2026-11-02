using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.Beautician.Queries.AllBeautyServices
{
    public record GetAllBeautyServiceByQuery
    (PaginationRequest PaginationRequest):IRequest<Result<PagedResult<GetAllBeautyServiceByQueryResponse>>>;
}

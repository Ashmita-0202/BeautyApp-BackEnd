using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.Beautician.Queries.GetAllBeautician
{
    public record  GetAllBeauticianByQuery
    (PaginationRequest PaginationRequest):IRequest<Result<PagedResult<GetAllBeauticianByQueryResponse>>>;
}

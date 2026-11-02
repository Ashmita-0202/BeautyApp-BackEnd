using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Payment.Application.Payment.Query.GetAllPayment
{
    public record  GetAllPaymentByQuery
    (PaginationRequest PaginationRequest):IRequest<Result<PagedResult<GetAllPaymentByQueryResponse>>>;
}

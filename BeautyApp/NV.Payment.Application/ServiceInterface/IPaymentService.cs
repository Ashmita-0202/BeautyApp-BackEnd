using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NV.Payment.Application.Payment.Command.AddPayment;
using NV.Payment.Application.Payment.Command.UpdatePayment;
using NV.Payment.Application.Payment.Query.GetAllPayment;
using NV.Payment.Application.Payment.Query.GetPaymentById;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Payment.Application.ServiceInterface
{
    public interface IPaymentService
    {
        Task<Result<AddPaymentResponse>> AddPayment(AddPaymentCommand command);
        Task<Result<PagedResult<GetAllPaymentByQueryResponse>>> GetAllPayment(PaginationRequest paginationRequest, CancellationToken cancellationToken = default);
        Task<Result<bool>> Delete(string id, CancellationToken cancellationToken);
        Task<Result<GetPaymentByIdQueryResponse>> GetPaymentById(string id, CancellationToken cancellationToken = default);

        Task<Result<UpdatePaymentResponse>> Update(string id, UpdatePaymentByCommand updatePaymentByCommand);
    }
}

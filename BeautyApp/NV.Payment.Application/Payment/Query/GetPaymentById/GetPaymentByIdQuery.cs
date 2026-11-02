using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Payment.Application.Payment.Query.GetPaymentById
{
    public record  GetPaymentByIdQuery
    (string id):IRequest<Result<GetPaymentByIdQueryResponse>>;
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Inventory.Application.Inventory.Query.ProductById
{
    public record GetProductByIdQuery 
    (string id): IRequest<Result<GetProductByIdQueryResponse>>;

}

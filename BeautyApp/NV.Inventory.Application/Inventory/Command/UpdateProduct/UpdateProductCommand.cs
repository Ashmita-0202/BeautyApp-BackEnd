using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;

namespace NV.Inventory.Application.Inventory.Command.UpdateProduct
{
    public record  UpdateProductCommand
    (
            string id,
            string name,
            int quantity,
            decimal price,
            DateTime latestUpdated
    ) :IRequest<Result<UpdateProductResponse>>;
}

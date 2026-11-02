using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using TN.Shared.Domain.Abstractions;

namespace NV.Inventory.Application.Inventory.Command.AddProduct
{
    public record  AddProductCommand
    (
        
            string name,
            int quantity,
            decimal price,
            DateTime latestUpdated
        
    ) :IRequest<Result<AddProductResponse>>;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Inventory.Application.Inventory.Command.AddProduct.RequestCommandMapper
{
    public static class AddProductRequestMapper
    {
        public static AddProductCommand ToCommand(this AddProductRequest request)
        {
            return new AddProductCommand
                (
                    request.name,
                    request.quantity,
                    request.price,
                    request.latestUpdated
                
                );
        }
    }
}

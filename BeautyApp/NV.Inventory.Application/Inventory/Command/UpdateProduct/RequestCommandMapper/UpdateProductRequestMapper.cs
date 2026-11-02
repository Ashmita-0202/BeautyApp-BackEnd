using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Inventory.Application.Inventory.Command.UpdateProduct.RequestCommandMapper
{
    public static  class UpdateProductRequestMapper
    {
        public static UpdateProductCommand ToCommand(this UpdateProductRequest request, string id) 
        {
            return new UpdateProductCommand
                (
                
                    id,
                    request.name,
                    request.quantity,
                    request.price,
                    request.latestUpdated
                
                );
        
        }
    }
}

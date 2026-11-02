using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Inventory.Application.Inventory.Query.GetAllProduct
{
    public record  GetAllProductByQueryResponse
    (
            string id,
            string name,
            int quantity,
            decimal price,
            DateTime latestUpdated
    );
    
}



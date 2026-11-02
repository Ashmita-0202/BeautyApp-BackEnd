using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Inventory.Application.Inventory.Command.UpdateProduct
{
    public record UpdateProductResponse
   (

            string id,
            string name,
            int quantity,
            decimal price,
            DateTime latestUpdated
    );
}

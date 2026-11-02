using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Queries.GetAllBeautician
{
    public record  GetAllBeauticianByQueryResponse
    (        
            string id="",
            int experienceYear=0,
            string skills="",
            string userId = ""
        
    );
}

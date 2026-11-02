using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautician
{
   public record AddBeauticianResponse
   (
             string id,
            int experienceYear,
            string skills,
            string userId
   );
}

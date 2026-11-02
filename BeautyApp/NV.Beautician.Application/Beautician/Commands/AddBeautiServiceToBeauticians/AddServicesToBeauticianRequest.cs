using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians
{
    public record  AddServicesToBeauticianRequest
    (
         string beauticianId,
         List<string> beautiServiceId
    );
}

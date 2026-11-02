using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautyService
{
    public record  UpdateBeautyServiceResponse
   (
            string id,
            string name,
            string description,
            decimal price,
            int durationInMinutes
    );
}

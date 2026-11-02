using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautyServices.RequestCommandMapper
{
    public static class AddBeautyServicesRequestMapper
    {
        public static AddBeautyServicesCommand ToCommand(this AddBeautyServicesRequest request) 
        {
        
            return new AddBeautyServicesCommand
                (
                   request.name,
                   request.description,
                   request.price,
                   request.durationInMinutes
              
                );
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians.RequestCommandMapper
{
    public static class AddServiceToBeauticianRequestMapper
    {
        public static AddServicesToBeauticianCommand ToCommand(this AddServicesToBeauticianRequest request) 
        {
            return new AddServicesToBeauticianCommand
                (

                    request.beauticianId,
                    request.beautiServiceId
                );
        
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautyService.RequestCommandMapper
{
    public static class UpdateBeautyServiceRequestMapper
    {
        public static UpdateBeautyServiceCommand Tocommand(this UpdateBeautyServiceRequest request, string id)
        {
            return new UpdateBeautyServiceCommand
                (
               
                   id,
                    request.name,
                    request.description,
                    request.price,
                    request.durationInMinutes
                
                
                
                );
        }
    }
}

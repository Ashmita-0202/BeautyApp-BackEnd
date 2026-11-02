using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautician.RequestCommandMapper
{
    public static class UpdateBeauticianRequestMapper
    {

        public static UpdateBeauticianCommand ToCommand(this UpdateBeauticianRequest request, string id)
        {
            return new UpdateBeauticianCommand
                (
                  id,
                 request.experienceYear,
                 request.skills,
                 request.userId

                );
        }
    }
}

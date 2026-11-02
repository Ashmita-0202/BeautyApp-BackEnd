using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautician.RequestCommandMapper
{
    public static class AddBeauticianRequestMapper
    {
        public static AddBeauticianCommand ToCommand(this AddBeauticianRequest  request) 
        {
            return new AddBeauticianCommand
           (
                
               request.experienceYear,
               request.skills,
               request.userId
                
           );
        
        }
    }
}

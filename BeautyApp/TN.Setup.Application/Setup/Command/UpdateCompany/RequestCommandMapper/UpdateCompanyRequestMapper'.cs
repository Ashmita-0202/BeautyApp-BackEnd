using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.Setup.Application.Setup.Command.UpdateOrganization;

namespace TN.Setup.Application.Setup.Command.UpdateCompany.RequestCommandMapper
{
   public static class UpdateCompanyRequestMapper_
    {
        public static UpdateCompanyCommand ToCommand(this UpdateCompanyRequest request, string organizationId)
        { 
            return new UpdateCompanyCommand
                (
                      organizationId,
                      request.name,
                      request.address,
                      request.email,
                      request.contactNumber,
                      request.pan,
                      request.imageUrl,
                      request.isEnabled,
                      request.isDeleted,
                      request.organizationId
                
                );

        }

    }
}

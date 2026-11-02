

namespace TN.Setup.Application.Setup.Command.AddCompany.RequestCommandMapper
{
    public static class AddCompanyRequestMapper
    {
        public static AddCompanyCommand ToCommand(this AddCompanyRequest addCompanyRequest)
        {
          return new AddCompanyCommand
                     (

                       addCompanyRequest.name,
                        addCompanyRequest.address,
                       addCompanyRequest.email,
                        addCompanyRequest.contactNumber,
                       addCompanyRequest.pan,
                       addCompanyRequest.imageUrl,
                        addCompanyRequest.isEnabled,
                        addCompanyRequest.isDeleted,
                        addCompanyRequest.organizationId

                      );


        }
    }
}

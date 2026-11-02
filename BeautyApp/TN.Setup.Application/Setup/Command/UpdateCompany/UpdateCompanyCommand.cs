using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TN.Shared.Domain.Abstractions;


namespace TN.Setup.Application.Setup.Command.UpdateCompany
{
    public record UpdateCompanyCommand
    (
            string id,
            string name,
            string address,
            string email,
            string contactNumber,
            string pan,
            string imageUrl,
            bool isEnabled,
            bool isDeleted,
            string organizationId
    ) :IRequest<Result<UpdateCompanyResponse>>;
}

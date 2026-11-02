
using MediatR;
using TN.Shared.Domain.Abstractions;


namespace TN.Setup.Application.Setup.Command.AddCompany
{
    public record AddCompanyCommand
    (
            string name,
            string address,
            string email,
            string contactNumber,
            string pan,
            string imageUrl,
            bool isEnabled,
            bool isDeleted,
            string organizationId
    ) : IRequest<Result<AddCompanyResponse>>;
}

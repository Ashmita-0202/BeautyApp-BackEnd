using AutoMapper;
using MediatR;
using TN.Setup.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace TN.Setup.Application.Setup.Queries.Modules;

public sealed class GetAllModulesQueryHandler(IModule module, IMapper mapper)
    : IRequestHandler<GetAllModulesQuery, Result<PagedResult<GetAllModulesResponse>>>
{
    public async Task<Result<PagedResult<GetAllModulesResponse>>> Handle(GetAllModulesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var allModule = await module.GetAllModule(request.PaginationRequest, cancellationToken);
            var allModuleDisplay = mapper.Map<PagedResult<GetAllModulesResponse>>(allModule.Data);

            return Result<PagedResult<GetAllModulesResponse>>.Success(allModuleDisplay);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching all modules", ex);
        }
    }
}

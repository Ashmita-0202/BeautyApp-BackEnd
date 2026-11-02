using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using MediatR;
using NV.Beautician.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.Beautician.Queries.GetAllBeautician
{
    public  sealed class GetAllBeauticianByQueryHandler:IRequestHandler<GetAllBeauticianByQuery, Result<PagedResult<GetAllBeauticianByQueryResponse>>>
    {
        private readonly IBeauticianService _beauticianService;
        private readonly IMapper _mapper;

        public GetAllBeauticianByQueryHandler(IBeauticianService beauticianService,IMapper mapper)
        {
            _beauticianService = beauticianService;
            _mapper=mapper;
        }

        public async Task<Result<PagedResult<GetAllBeauticianByQueryResponse>>> Handle(GetAllBeauticianByQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _beauticianService.GetAllBeautician(request.PaginationRequest);

                var beauticianResponse = _mapper.Map<PagedResult<GetAllBeauticianByQueryResponse>>(result.Data);

                return Result<PagedResult<GetAllBeauticianByQueryResponse>>.Success(beauticianResponse);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<GetAllBeauticianByQueryResponse>>.Failure($"An error occurred: {ex.Message}");
            }

        }
    }
}

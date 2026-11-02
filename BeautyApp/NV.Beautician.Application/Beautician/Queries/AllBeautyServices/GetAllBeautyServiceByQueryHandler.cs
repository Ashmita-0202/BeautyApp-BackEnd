using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.ServiceInterface;
using NV.Buautician.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Beautician.Application.Beautician.Queries.AllBeautyServices
{
    public class GetAllBeautyServiceByQueryHandler:IRequestHandler<GetAllBeautyServiceByQuery, Result<PagedResult<GetAllBeautyServiceByQueryResponse>>>
    {
        private readonly IBeautyServices _beautyServices;
        private readonly IMapper _mapper;

        public GetAllBeautyServiceByQueryHandler(IBeautyServices beautyServices,IMapper mapper)
        {
            _beautyServices=beautyServices;
            _mapper=mapper;
        }

        public async Task<Result<PagedResult<GetAllBeautyServiceByQueryResponse>>> Handle(GetAllBeautyServiceByQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _beautyServices.GetAllBeautyService(request.PaginationRequest);

                var beautyResponse = _mapper.Map<PagedResult<GetAllBeautyServiceByQueryResponse>>(result.Data);

                return Result<PagedResult<GetAllBeautyServiceByQueryResponse>>.Success(beautyResponse);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<GetAllBeautyServiceByQueryResponse>>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}

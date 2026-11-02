using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Beautician.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Queries.GetBeauticianById
{
    public  class GetBeauticianByIdQueryHandler : IRequestHandler<GetBeauticianByIdQuery, Result<GetBeauticianByIdQueryResponse>>
    {
        private readonly IBeauticianService _beauticianService;
        private readonly IMapper _mapper;

        public GetBeauticianByIdQueryHandler(IBeauticianService beauticianService, IMapper mapper)
        {
            _beauticianService=beauticianService;
            _mapper=mapper;
        }

        public async Task<Result<GetBeauticianByIdQueryResponse>> Handle(GetBeauticianByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var beauticianById = await _beauticianService.GetBeauticiansById(request.id);

                return Result<GetBeauticianByIdQueryResponse>.Success(beauticianById.Data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Beauticians by Id", ex);

            }
        }
    }
}

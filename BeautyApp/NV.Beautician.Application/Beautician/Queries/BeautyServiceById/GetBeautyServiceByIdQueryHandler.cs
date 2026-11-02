using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Beautician.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Queries.BeautyServiceById
{
    public  class GetBeautyServiceByIdQueryHandler:IRequestHandler<GetBeautyServiceByIdQuery,Result<GetBeautyServiceByIdQueryResponse>>
    {
        private readonly IBeautyServices _beautyServices;
        private readonly IMapper _mapper;

        public GetBeautyServiceByIdQueryHandler(IBeautyServices beautyServices,IMapper mapper)
        {
            _beautyServices=beautyServices;
            _mapper=mapper;
        }

        public async Task<Result<GetBeautyServiceByIdQueryResponse>> Handle(GetBeautyServiceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var beautyServiceById = await _beautyServices.GetBeautyServiceById(request.id);

                return Result<GetBeautyServiceByIdQueryResponse>.Success(beautyServiceById.Data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Beauty Service by Id", ex);

            }
        }
    }
}

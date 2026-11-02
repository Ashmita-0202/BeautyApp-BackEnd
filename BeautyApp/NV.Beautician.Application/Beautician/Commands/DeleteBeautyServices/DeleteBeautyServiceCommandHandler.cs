using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Beautician.Application.ServiceInterface;
using NV.Buautician.Domain.Entities;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.DeleteBeautyServices
{
    public class DeleteBeautyServiceCommandHandler:IRequestHandler<DeleteBeautyServiceCommand,Result<bool>>
    {
        private readonly IBeautyServices _beautyServices;
        private readonly IMapper _mapper;

        public DeleteBeautyServiceCommandHandler(IBeautyServices beautyServices,IMapper mapper)
        {
            _beautyServices=beautyServices;
            _mapper=mapper;
        }

        public async Task<Result<bool>> Handle(DeleteBeautyServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteBeautyService = await _beautyServices.Delete(request.id, cancellationToken);
                if (deleteBeautyService is null)
                {
                    return Result<bool>.Failure("NotFound", "beauty services not Found");
                }
                return Result<bool>.Success(true);


            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting {request.id}", ex);
            }
        }
    }
}

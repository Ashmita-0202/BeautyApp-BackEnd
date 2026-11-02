using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Beautician.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.DeleteBeautician
{
    public class DeleteBeauticianCommandHandler:IRequestHandler<DeleteBeauticianCommand, Result<bool>>
    {
        private readonly IBeauticianService _beauticianService;
        private readonly IMapper _mapper;

        public DeleteBeauticianCommandHandler(IBeauticianService beauticianService,IMapper mapper)
        { 
            _beauticianService=beauticianService;
            _mapper=mapper;
        }

        public async Task<Result<bool>> Handle(DeleteBeauticianCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteBeautician = await _beauticianService.Delete(request.id, cancellationToken);
                if (deleteBeautician is null)
                {
                    return Result<bool>.Failure("NotFound", "Beauticians not Found");
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

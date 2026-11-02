using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NV.Beautician.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautyService
{
    public  class UpdateBeautyServiceCommandHandler:IRequestHandler<UpdateBeautyServiceCommand, Result<UpdateBeautyServiceResponse>>
    {
        private readonly IBeautyServices _beautyServices;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateBeautyServiceCommand> _validator;

        public UpdateBeautyServiceCommandHandler(IBeautyServices beautyServices,IMapper mapper,IValidator<UpdateBeautyServiceCommand> validator) 
        {
            _beautyServices=beautyServices;
            _mapper=mapper;
            _validator=validator;
        
        }

        public async Task<Result<UpdateBeautyServiceResponse>> Handle(UpdateBeautyServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var validationAction = await _validator.ValidateAsync(request, cancellationToken);
                if ((!validationAction.IsValid))
                {
                    var errors = string.Join(", ", validationAction.Errors.Select(x => x.ErrorMessage));
                    return Result<UpdateBeautyServiceResponse>.Failure(errors);

                }

                var updateBeautyService = await _beautyServices.Update(request.id, request);

                if (updateBeautyService.Errors.Any())
                {
                    var errors = string.Join(", ", updateBeautyService.Errors);
                    return Result<UpdateBeautyServiceResponse>.Failure(errors);
                }

                if (updateBeautyService is null || !updateBeautyService.IsSuccess)
                {
                    return Result<UpdateBeautyServiceResponse>.Failure("Updates BeautyService failed");
                }

                var beautyServiceDisplay = _mapper.Map<UpdateBeautyServiceResponse>(updateBeautyService.Data);
                return Result<UpdateBeautyServiceResponse>.Success(beautyServiceDisplay);
            }
            catch (Exception ex)
            {
                throw new Exception("$\"An error occurred while updating beauty Service by {request.id}", ex);
            }
        }
    }
}

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

namespace NV.Beautician.Application.Beautician.Commands.UpdateBeautician
{
    public  class UpdateBeauticianCommandHandler:IRequestHandler<UpdateBeauticianCommand,Result<UpdateBeauticianResponse>>
    {
        private readonly IBeauticianService _beauticianService;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateBeauticianCommand> _validator;

        public UpdateBeauticianCommandHandler(IBeauticianService beauticianService, IMapper mapper, IValidator<UpdateBeauticianCommand> validator)
        { 
            _beauticianService=beauticianService;
            _mapper=mapper;
            _validator=validator;
        
        }

        public async Task<Result<UpdateBeauticianResponse>> Handle(UpdateBeauticianCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var validationAction = await _validator.ValidateAsync(request, cancellationToken);
                if ((!validationAction.IsValid))
                {
                    var errors = string.Join(", ", validationAction.Errors.Select(x => x.ErrorMessage));
                    return Result<UpdateBeauticianResponse>.Failure(errors);

                }

                var updateBeautician = await _beauticianService.Update(request.id, request);

                if (updateBeautician.Errors.Any())
                {
                    var errors = string.Join(", ", updateBeautician.Errors);
                    return Result<UpdateBeauticianResponse>.Failure(errors);
                }

                if (updateBeautician is null || !updateBeautician.IsSuccess)
                {
                    return Result<UpdateBeauticianResponse>.Failure("Updates Beautician failed");
                }

                var beauticianDisplay = _mapper.Map<UpdateBeauticianResponse>(updateBeautician.Data);
                return Result<UpdateBeauticianResponse>.Success(beauticianDisplay);
            }
            catch (Exception ex)
            {
                throw new Exception("$\"An error occurred while updating Beauticians by {request.id}", ex);
            }
        }
    }
}

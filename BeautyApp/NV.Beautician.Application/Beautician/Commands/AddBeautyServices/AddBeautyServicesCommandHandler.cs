using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Beautician.Application.Beautician.Commands.AddBeautyServices
{
    public class AddBeautyServicesCommandHandler:IRequestHandler<AddBeautyServicesCommand,Result<AddBeautyServicesResponse>>
    {
        private readonly IBeautyServices _beautyServices;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBeautyServicesCommand> _validator;
        private readonly IBeauticianService? beauticianService;

        public AddBeautyServicesCommandHandler(IBeautyServices beautyServices,IMapper mapper,IValidator<AddBeautyServicesCommand> validator)
        {
           _beautyServices=beautyServices;
            _mapper=mapper;
            _validator=validator;
        }

        public async Task<Result<AddBeautyServicesResponse>> Handle(AddBeautyServicesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                    return Result<AddBeautyServicesResponse>.Failure(errors);
                }

                var addBeautyServices = await _beautyServices.Add(request);

                if (addBeautyServices.Errors.Any())
                {
                    var errors = string.Join(", ", addBeautyServices.Errors);
                    return Result<AddBeautyServicesResponse>.Failure(errors);
                }

                if (addBeautyServices is null || !addBeautyServices.IsSuccess)
                {
                    return Result<AddBeautyServicesResponse>.Failure(" ");
                }

                var beautyServicesDisplay = _mapper.Map<AddBeautyServicesResponse>(addBeautyServices.Data);
                return Result<AddBeautyServicesResponse>.Success(beautyServicesDisplay);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Beauty Services", ex);

            }
        }
    }
}

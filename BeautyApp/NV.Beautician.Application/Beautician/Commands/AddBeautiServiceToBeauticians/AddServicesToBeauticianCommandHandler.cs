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

namespace NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians
{
    public class AddServicesToBeauticianCommandHandler:IRequestHandler<AddServicesToBeauticianCommand,Result<AddServicesToBeauticianResponse>>
    {
        private readonly IValidator<AddServicesToBeauticianCommand> _validator;
        private readonly IServicesBeautician _servicesBeautician;
        private readonly IMapper _mapper;

        public AddServicesToBeauticianCommandHandler(IServicesBeautician servicesBeautician, IMapper mapper,IValidator<AddServicesToBeauticianCommand> validator)
        {
            _servicesBeautician=servicesBeautician;
            _mapper=mapper;
            _validator=validator;
        }

        public async Task<Result<AddServicesToBeauticianResponse>> Handle(AddServicesToBeauticianCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return Result<AddServicesToBeauticianResponse>.Failure(errors);
                }

                if (request is null)
                {
                    return Result<AddServicesToBeauticianResponse>.Failure("Invalid request");
                }

                var result = await _servicesBeautician.Add(request);

                if (result is null || !result.IsSuccess)
                {
                    var errors = result?.Errors != null && result.Errors.Any()
                        ? string.Join(", ", result.Errors)
                        : "Failed to assign beauty services to beautician.";
                    return Result<AddServicesToBeauticianResponse>.Failure(errors);
                }

                var mappedResponse = _mapper.Map<AddServicesToBeauticianResponse>(request);

                if (mappedResponse is null)
                {
                    return Result<AddServicesToBeauticianResponse>.Failure("Failed to map response.");
                }

                return Result<AddServicesToBeauticianResponse>.Success(mappedResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while assigning beauty services to beautician", ex);
            }
        }
    }
}

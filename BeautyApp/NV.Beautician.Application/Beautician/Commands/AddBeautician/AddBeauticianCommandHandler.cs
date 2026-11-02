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

namespace NV.Beautician.Application.Beautician.Commands.AddBeautician
{
    public class AddBeauticianCommandHandler:IRequestHandler<AddBeauticianCommand, Result<AddBeauticianResponse>>
    {
        private readonly IBeauticianService _beauticianService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBeauticianCommand> _validator;

        public AddBeauticianCommandHandler(IBeauticianService beauticianService, IMapper mapper, IValidator<AddBeauticianCommand> validator)
        {
            _beauticianService=beauticianService;
            _mapper=mapper;
            _validator=validator;
        }

        public async Task<Result<AddBeauticianResponse>> Handle(AddBeauticianCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                    return Result<AddBeauticianResponse>.Failure(errors);
                }

                var addBeauticians = await  _beauticianService.Add(request);

                if (addBeauticians.Errors.Any())
                {
                    var errors = string.Join(", ", addBeauticians.Errors);
                    return Result<AddBeauticianResponse>.Failure(errors);
                }

                if (addBeauticians is null || !addBeauticians.IsSuccess)
                {
                    return Result<AddBeauticianResponse>.Failure(" ");
                }

                var beauticiansDisplay = _mapper.Map<AddBeauticianResponse>(addBeauticians.Data);
                return Result<AddBeauticianResponse>.Success(beauticiansDisplay);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Beauticians", ex);

            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NV.Payment.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Payment.Application.Payment.Command.UpdatePayment
{
    public class UpdatePaymentByCommandHandler:IRequestHandler<UpdatePaymentByCommand,Result<UpdatePaymentResponse>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePaymentByCommand> _validator;

        public UpdatePaymentByCommandHandler(IPaymentService paymentService, IMapper mapper,IValidator<UpdatePaymentByCommand> validator)
        {
            _paymentService=paymentService;
            _mapper=mapper;
             _validator=validator;
        
        }

        public async Task<Result<UpdatePaymentResponse>> Handle(UpdatePaymentByCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var validationAction = await _validator.ValidateAsync(request, cancellationToken);
                if ((!validationAction.IsValid))
                {
                    var errors = string.Join(", ", validationAction.Errors.Select(x => x.ErrorMessage));
                    return Result<UpdatePaymentResponse>.Failure(errors);

                }

                var updatePayment = await _paymentService.Update(request.id, request);

                if (updatePayment.Errors.Any())
                {
                    var errors = string.Join(", ", updatePayment.Errors);
                    return Result<UpdatePaymentResponse>.Failure(errors);
                }

                if (updatePayment is null || !updatePayment.IsSuccess)
                {
                    return Result<UpdatePaymentResponse>.Failure("Updates payment failed");
                }

                var paymentDisplay = _mapper.Map<UpdatePaymentResponse>(updatePayment.Data);
                return Result<UpdatePaymentResponse>.Success(paymentDisplay);
            }
            catch (Exception ex)
            {
                throw new Exception("$\"An error occurred while updating Payment by {request.id}", ex);
            }
        }
    }
}

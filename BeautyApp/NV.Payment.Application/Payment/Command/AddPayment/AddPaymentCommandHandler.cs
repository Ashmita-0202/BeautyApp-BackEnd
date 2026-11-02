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

namespace NV.Payment.Application.Payment.Command.AddPayment
{
    public  class AddPaymentCommandHandler: IRequestHandler<AddPaymentCommand, Result<AddPaymentResponse>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddPaymentCommand> _validator;

        public AddPaymentCommandHandler(IPaymentService paymentService, IMapper mapper, IValidator<AddPaymentCommand> validator)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            _validator = validator;
       
        }

        public async Task<Result<AddPaymentResponse>> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                    return Result<AddPaymentResponse>.Failure(errors);
                }

                var addPayment = await _paymentService.AddPayment(request);

                if (addPayment.Errors.Any())
                {
                    var errors = string.Join(", ", addPayment.Errors);
                    return Result<AddPaymentResponse>.Failure(errors);
                }

                if (addPayment is null || !addPayment.IsSuccess)
                {
                    return Result<AddPaymentResponse>.Failure(" ");
                }

                var paymentDisplay = _mapper.Map<AddPaymentResponse>(addPayment.Data);
                return Result<AddPaymentResponse>.Success(paymentDisplay);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Payment", ex);

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Payment.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Payment.Application.Payment.Command.DeletePayment
{
    public class  DeletePaymentCommandHandler:IRequestHandler<DeletePaymentCommand, Result<bool>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public DeletePaymentCommandHandler(IPaymentService paymentService,IMapper mapper)
        {
            _paymentService=paymentService;
            _mapper=mapper;
            
        }

        public async Task<Result<bool>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletePayment = await _paymentService.Delete(request.id, cancellationToken);
                if (deletePayment is null)
                {
                    return Result<bool>.Failure("NotFound", "Payment not Found");
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

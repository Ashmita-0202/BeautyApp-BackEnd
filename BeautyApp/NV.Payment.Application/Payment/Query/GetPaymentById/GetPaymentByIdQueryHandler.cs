using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Payment.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Payment.Application.Payment.Query.GetPaymentById
{
    public class GetPaymentByIdQueryHandler:IRequestHandler<GetPaymentByIdQuery,Result<GetPaymentByIdQueryResponse>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public GetPaymentByIdQueryHandler(IPaymentService paymentService,IMapper mapper) 
        {
            _paymentService=paymentService;
            _mapper=mapper;
        
        }

        public async Task<Result<GetPaymentByIdQueryResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var paymentById = await _paymentService.GetPaymentById(request.id);

                return Result<GetPaymentByIdQueryResponse>.Success(paymentById.Data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Payment by Id", ex);

            }
        }
    }
}

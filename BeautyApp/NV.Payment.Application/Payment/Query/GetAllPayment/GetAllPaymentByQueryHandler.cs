using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Payment.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Payment.Application.Payment.Query.GetAllPayment
{
    public class  GetAllPaymentByQueryHandler:IRequestHandler<GetAllPaymentByQuery, Result<PagedResult<GetAllPaymentByQueryResponse>>>
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public GetAllPaymentByQueryHandler(IPaymentService paymentService,IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
            
        }

        public async Task<Result<PagedResult<GetAllPaymentByQueryResponse>>> Handle(GetAllPaymentByQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _paymentService.GetAllPayment(request.PaginationRequest);

                var paymentResponse = _mapper.Map<PagedResult<GetAllPaymentByQueryResponse>>(result.Data);

                return Result<PagedResult<GetAllPaymentByQueryResponse>>.Success(paymentResponse);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<GetAllPaymentByQueryResponse>>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}

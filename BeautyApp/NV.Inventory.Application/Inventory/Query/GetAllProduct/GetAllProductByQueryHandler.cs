using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Inventory.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Inventory.Application.Inventory.Query.GetAllProduct
{
    public  class GetAllProductByQueryHandler:IRequestHandler<GetAllProductByQuery, Result<PagedResult<GetAllProductByQueryResponse>>>
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;

        public GetAllProductByQueryHandler(IProductServices productServices, IMapper mapper)
        {
            _productServices=productServices;
            _mapper=mapper;
            
        }

        public  async Task<Result<PagedResult<GetAllProductByQueryResponse>>> Handle(GetAllProductByQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var result = await _productServices.GetAllProduct(request.PaginationRequest);

                var productResponse = _mapper.Map<PagedResult<GetAllProductByQueryResponse>>(result.Data);

                return Result<PagedResult<GetAllProductByQueryResponse>>.Success(productResponse);
            }
            catch (Exception ex)
            {
                return Result<PagedResult<GetAllProductByQueryResponse>>.Failure($"An error occurred: {ex.Message}");
            }
        }
    }
}

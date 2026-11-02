using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Inventory.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Inventory.Application.Inventory.Query.ProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdQueryResponse>>
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductServices productServices, IMapper mapper) 
        {
            _productServices= productServices;
            _mapper= mapper;
        
        }

        public async Task<Result<GetProductByIdQueryResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {

                var productById = await _productServices.GetProductById(request.id);

                return Result<GetProductByIdQueryResponse>.Success(productById.Data);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching Product by Id", ex);

            }
        }
    }
}

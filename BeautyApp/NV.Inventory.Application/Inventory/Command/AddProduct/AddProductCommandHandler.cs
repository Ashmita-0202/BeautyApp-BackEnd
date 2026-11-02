using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using NV.Inventory.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Inventory.Application.Inventory.Command.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result<AddProductResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IProductServices _productServices;
        private readonly IValidator<AddProductCommand> _validator;

        public AddProductCommandHandler(IProductServices productServices,IMapper mapper,IValidator<AddProductCommand> validator)
        {
            _mapper=mapper;
            _productServices=productServices;
            _validator=validator;
        
        }
        public async Task<Result<AddProductResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                    return Result<AddProductResponse>.Failure(errors);
                }

                var addProduct = await _productServices.Add(request);

                if (addProduct.Errors.Any())
                {
                    var errors = string.Join(", ", addProduct.Errors);
                    return Result<AddProductResponse>.Failure(errors);
                }

                if (addProduct is null || !addProduct.IsSuccess)
                {
                    return Result<AddProductResponse>.Failure(" ");
                }

                var productDisplay = _mapper.Map<AddProductResponse>(addProduct.Data);
                return Result<AddProductResponse>.Success(productDisplay);


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Product", ex);

            }
        }
    }
}

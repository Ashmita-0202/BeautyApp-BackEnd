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

namespace NV.Inventory.Application.Inventory.Command.UpdateProduct
{
    public class UpdateProductCommandHandler:IRequestHandler<UpdateProductCommand, Result<UpdateProductResponse>>
    {
        private readonly IProductServices _productServices;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateProductCommand> _validator;

        public UpdateProductCommandHandler(IProductServices productServices,IMapper mapper,IValidator<UpdateProductCommand> validator)
        {
            _productServices=productServices;
            _mapper=mapper;
            _validator=validator;
            
        }

        public async Task<Result<UpdateProductResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var validationAction = await _validator.ValidateAsync(request, cancellationToken);
                if ((!validationAction.IsValid))
                {
                    var errors = string.Join(", ", validationAction.Errors.Select(x => x.ErrorMessage));
                    return Result<UpdateProductResponse>.Failure(errors);

                }

                var updateProduct = await _productServices.Update(request.id, request);

                if (updateProduct.Errors.Any())
                {
                    var errors = string.Join(", ", updateProduct.Errors);
                    return Result<UpdateProductResponse>.Failure(errors);
                }

                if (updateProduct is null || !updateProduct.IsSuccess)
                {
                    return Result<UpdateProductResponse>.Failure("Updates product failed");
                }

                var beauticianDisplay = _mapper.Map<UpdateProductResponse>(updateProduct.Data);
                return Result<UpdateProductResponse>.Success(beauticianDisplay);
            }
            catch (Exception ex)
            {
                throw new Exception("$\"An error occurred while updating product by {request.id}", ex);
            }
        }
    }
}

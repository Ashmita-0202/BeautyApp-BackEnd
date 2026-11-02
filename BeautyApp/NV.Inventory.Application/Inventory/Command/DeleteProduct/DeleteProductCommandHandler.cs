using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NV.Inventory.Application.ServiceInterface;
using TN.Shared.Domain.Abstractions;

namespace NV.Inventory.Application.Inventory.Command.DeleteProduct
{
    public class DeleteProductCommandHandler:IRequestHandler<DeleteProductCommand,Result<bool>>
    {
        private readonly IProductServices _prdocutServices;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductServices productServices,IMapper mapper)
        {
            _prdocutServices=productServices;
            _mapper=mapper;
        }

        public async  Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deleteProduct = await _prdocutServices.Delete(request.id, cancellationToken);
                if (deleteProduct is null)
                {
                    return Result<bool>.Failure("NotFound", "product not Found");
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

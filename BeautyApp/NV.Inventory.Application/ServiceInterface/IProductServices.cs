using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NV.Inventory.Application.Inventory.Command.AddProduct;
using NV.Inventory.Application.Inventory.Command.UpdateProduct;
using NV.Inventory.Application.Inventory.Query.GetAllProduct;
using NV.Inventory.Application.Inventory.Query.ProductById;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Inventory.Application.ServiceInterface
{
    public interface IProductServices
    {
        Task<Result<AddProductResponse>> Add(AddProductCommand addProductCommand);
        Task<Result<PagedResult<GetAllProductByQueryResponse>>> GetAllProduct(PaginationRequest paginationRequest, CancellationToken cancellationToken=default);
 
        Task<Result<GetProductByIdQueryResponse>> GetProductById(string id, CancellationToken cancellationToken=default);
    
        Task<Result<UpdateProductResponse>> Update(string id,UpdateProductCommand updateProductCommand);
        Task<Result<bool>> Delete(string id, CancellationToken cancellationToken);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NV.Inventory.Application.Inventory.Command.AddProduct;
using NV.Inventory.Application.Inventory.Command.UpdateProduct;
using NV.Inventory.Application.Inventory.Query.GetAllProduct;
using NV.Inventory.Application.Inventory.Query.ProductById;
using NV.Inventory.Application.ServiceInterface;
using NV.Inventory.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Shared.Domain.IRepository;

namespace NV.Inventory.Infrastructure.ServiceImpl
{
    public class ProductServices:IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductServices(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        
        }

        public async Task<Result<AddProductResponse>> Add(AddProductCommand addProductCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {

                    string newId = Guid.NewGuid().ToString();
                    var productData = new Product
                    (
                            newId,
                            addProductCommand.name,
                            addProductCommand.quantity,
                            addProductCommand.price,
                            addProductCommand.latestUpdated
                    );

                    await _unitOfWork.BaseRepository<Product>().AddAsync(productData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddProductResponse>(productData);
                    return Result<AddProductResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding Product ", ex);

                }
            }
        }

        public async Task<Result<bool>> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.BaseRepository<Product>().GetByGuIdAsync(id);
                if (products is null)
                {
                    return Result<bool>.Failure("NotFound", "Product Cannot be Found");
                }

                _unitOfWork.BaseRepository<Product>().Delete(products);
                await _unitOfWork.SaveChangesAsync();


                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting Product having {id}", ex);
            }
        }

        public async Task<Result<PagedResult<GetAllProductByQueryResponse>>> GetAllProduct(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _unitOfWork.BaseRepository<Product>().GetAllAsyncWithPagination();

                var productPagedResult = await product.AsNoTracking().ToPagedResultAsync(paginationRequest.pageIndex, paginationRequest.pageSize, paginationRequest.IsPagination);


                var AllProductResponse = _mapper.Map<PagedResult<GetAllProductByQueryResponse>>(productPagedResult.Data);
                return Result<PagedResult<GetAllProductByQueryResponse>>.Success(AllProductResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all product  ", ex);
            }
        }

        public async Task<Result<GetProductByIdQueryResponse>> GetProductById(string id, CancellationToken cancellationToken = default)
        {
            try
            {

                var product = await _unitOfWork.BaseRepository<Product>().GetByGuIdAsync(id);
                var productResponse = _mapper.Map<GetProductByIdQueryResponse>(product);
                return Result<GetProductByIdQueryResponse>.Success(productResponse);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching product by using Id", ex);
            }
        }

        public async Task<Result<UpdateProductResponse>> Update(string id, UpdateProductCommand updateProductCommand)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (id == null)
                    {
                        return Result<UpdateProductResponse>.Failure("NotFound", "Please provide valid Products Id");
                    }

                    var productToBeUpdated = await _unitOfWork.BaseRepository<Product>().GetByGuIdAsync(id);
                    if (productToBeUpdated is null)
                    {
                        return Result<UpdateProductResponse>.Failure("NotFound", "Products are not Found");
                    }

                    _mapper.Map(updateProductCommand, productToBeUpdated);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultResponse = new UpdateProductResponse
                        (

                          id,
                          productToBeUpdated.Name,
                          productToBeUpdated.Quantity,
                          productToBeUpdated.Price,
                          productToBeUpdated.LatestUpdated
                         
                        );

                    return Result<UpdateProductResponse>.Success(resultResponse);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the Products", ex);
                }

            }
        }
    }
}

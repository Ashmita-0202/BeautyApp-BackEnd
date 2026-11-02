using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NV.Inventory.Application.Inventory.Command.AddProduct;
using NV.Inventory.Application.Inventory.Command.DeleteProduct;
using NV.Inventory.Application.Inventory.Command.UpdateProduct;
using NV.Inventory.Application.Inventory.Query.GetAllProduct;
using NV.Inventory.Application.Inventory.Query.ProductById;
using NV.Inventory.Domain.Entities;
using TN.Shared.Domain.Abstractions;
using TN.Shared.Domain.ExtensionMethod.Pagination;

namespace NV.Inventory.Application.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            #region Product

            CreateMap<AddProductResponse, Product>().ReverseMap();
            CreateMap<AddProductCommand, Product>().ReverseMap();
            CreateMap<GetAllProductByQueryResponse, Product>().ReverseMap();
            CreateMap<PagedResult<Product>, PagedResult<GetAllProductByQueryResponse>>().ReverseMap();
            CreateMap<GetProductByIdQueryResponse, Product>().ReverseMap();
            CreateMap<DeleteProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductCommand, Product>().ReverseMap();
            #endregion


        }
    }
}

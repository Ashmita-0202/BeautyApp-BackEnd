using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NV.Inventory.Application.Inventory.Command.AddProduct;
using NV.Inventory.Application.Inventory.Command.AddProduct.RequestCommandMapper;
using NV.Inventory.Application.Inventory.Command.DeleteProduct;
using NV.Inventory.Application.Inventory.Command.UpdateProduct;
using NV.Inventory.Application.Inventory.Command.UpdateProduct.RequestCommandMapper;
using NV.Inventory.Application.Inventory.Query.GetAllProduct;
using NV.Inventory.Application.Inventory.Query.ProductById;
using TN.Authentication.Application.ServiceInterface;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Web.Controllers;

namespace NV.Web.Area.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryControllers : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;

        public InventoryControllers(IMediator mediator, IUserServices userServices, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(userManager, roleManager)
        {
            _mediator = mediator;
            _userServices = userServices;
        }

        #region Inventory

        #region AddProduct
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addProductResult = await _mediator.Send(command);
            #region Switch Statement
            return addProductResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddProduct), addProductResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = addProductResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addProductResult.Errors),
                _ => BadRequest("Invalid Fields for Add Product ")
            };

            #endregion
        }

        #endregion

        #region AllProduct
        [HttpGet("all-product")]
        public async Task<IActionResult> AllProduct([FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetAllProductByQuery(paginationRequest);
            var productResult = await _mediator.Send(query);
            #region Switch Statement
            return productResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(productResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = productResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(productResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region ProductById
        [HttpGet("Product/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] string id)
        {
            var query = new GetProductByIdQuery(id);
            var productResult = await _mediator.Send(query);
            #region Switch Statement
            return productResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(productResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = productResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(productResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion
        }
        #endregion

        #region DeleteProduct
        [HttpDelete("DeleteProduct/{id}")]

        public async Task<IActionResult> DeleteProduct([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);
            var DeleteProductResult = await _mediator.Send(command);
            #region Switch Statement
            return DeleteProductResult switch
            {
                { IsSuccess: true, Data: true } => NoContent(),
                { IsSuccess: true, Message: not null } => new JsonResult(new { Message = DeleteProductResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(DeleteProductResult.Errors),
                _ => BadRequest("Invalid Fields for Delete Product")
            };

            #endregion
        }
        #endregion

        #region UpdateProduct
        [HttpPatch("UpdateProduct/{id}")]

        public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] UpdateProductRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand(id);
            var updateProductResult = await _mediator.Send(command);
            #region Switch Statement
            return updateProductResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updateProductResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updateProductResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updateProductResult.Errors),
                _ => BadRequest("Invalid Fields for Assign Products")
            };

            #endregion

        }
        #endregion

        #endregion




    }
}

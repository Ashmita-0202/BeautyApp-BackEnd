using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NV.Payment.Application.Payment.Command.AddPayment;
using NV.Payment.Application.Payment.Command.AddPayment.RequestCommandMapper;
using NV.Payment.Application.Payment.Command.DeletePayment;
using NV.Payment.Application.Payment.Command.UpdatePayment;
using NV.Payment.Application.Payment.Command.UpdatePayment.RequestCommandMapper;
using NV.Payment.Application.Payment.Query.GetAllPayment;
using NV.Payment.Application.Payment.Query.GetPaymentById;
using TN.Authentication.Application.ServiceInterface;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Web.Controllers;

namespace NV.Web.Area.Payment.Controller
{
    public class PaymentControllers : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;
        public PaymentControllers(IMediator mediator, IUserServices userServices, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(userManager, roleManager)
        {
            _mediator = mediator;
            _userServices = userServices;
        }
        #region Payment 

        #region AddPayment
        [HttpPost("AddPayment")]
        public async Task<IActionResult> AddPayment([FromBody] AddPaymentRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addPaymentResult = await _mediator.Send(command);
            #region Switch Statement
            return addPaymentResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddPayment), addPaymentResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = addPaymentResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addPaymentResult.Errors),
                _ => BadRequest("Invalid Fields for Add Payment ")
            };

            #endregion
        }

        #endregion

        #region AllPayment
        [HttpGet("all-Payment")]
        public async Task<IActionResult> AllPayment([FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetAllPaymentByQuery(paginationRequest);
            var paymentResult = await _mediator.Send(query);
            #region Switch Statement
            return paymentResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(paymentResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = paymentResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(paymentResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region DeletePayment
        [HttpDelete("DeletePayment/{id}")]

        public async Task<IActionResult> DeletePayment([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeletePaymentCommand(id);
            var deletePaymentResult = await _mediator.Send(command);
            #region Switch Statement
            return deletePaymentResult switch
            {
                { IsSuccess: true, Data: true } => NoContent(),
                { IsSuccess: true, Message: not null } => new JsonResult(new { Message = deletePaymentResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(deletePaymentResult.Errors),
                _ => BadRequest("Invalid Fields for Delete Payment")
            };

            #endregion
        }
        #endregion

        #region PaymentById
        [HttpGet("GetPayment/{id}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] string id)
        {
            var query = new GetPaymentByIdQuery(id);
            var paymentResult = await _mediator.Send(query);
            #region Switch Statement
            return paymentResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(paymentResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = paymentResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(paymentResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion
        }
        #endregion

        #region UpdatePayment
        [HttpPatch("UpdatePayment/{id}")]

        public async Task<IActionResult> UpdatePayment([FromRoute] string id, [FromBody] UpdatePaymentRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand(id);
            var updatePaymentResult = await _mediator.Send(command);
            #region Switch Statement
            return updatePaymentResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updatePaymentResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updatePaymentResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updatePaymentResult.Errors),
                _ => BadRequest("Invalid Fields for Assign Payment")
            };

            #endregion

        }
        #endregion
        #endregion


    }
}

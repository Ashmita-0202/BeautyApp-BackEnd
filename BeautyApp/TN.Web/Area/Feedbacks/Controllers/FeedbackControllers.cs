using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NV.Booking.Application.Booking.Command.AddBooking.RequestMapper;
using NV.Feedbacks.Application.Command.AddReview;
using NV.Feedbacks.Application.Command.AddReview.RequestCommandMapper;
using NV.Feedbacks.Application.Feedbacks.Command.DeleteReview;
using NV.Feedbacks.Application.Feedbacks.Command.UpdateReview;
using NV.Feedbacks.Application.Feedbacks.Command.UpdateReview.RequestCommandMapper;
using NV.Feedbacks.Application.Feedbacks.Queries.AllReview;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByBeauticianId;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewById;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByProductId;
using TN.Authentication.Application.ServiceInterface;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Web.Controllers;

namespace NV.Web.Area.Feedbacks.Controllers
{
    public class FeedBackControllers : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;

        public FeedBackControllers(IMediator mediator, IUserServices userServices, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(userManager, roleManager)
        {
            _mediator = mediator;
            _userServices = userServices;
        }


        #region Review

        #region AddReview
        [HttpPost("AddReview")]

        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addReviewResult = await _mediator.Send(command);
            #region Switch Statement
            return addReviewResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddReview), addReviewResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = addReviewResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addReviewResult.Errors),
                _ => BadRequest("Invalid Fields for Add Review ")
            };

            #endregion
        }

        #endregion

        #region AllReview
        [HttpGet("all-Review")]
        public async Task<IActionResult> AllReview([FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetAllReviewByQuery(paginationRequest);
            var reviewResult = await _mediator.Send(query);
            #region Switch Statement
            return reviewResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(reviewResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = reviewResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(reviewResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region ReviewById
        [HttpGet("Review/{id}")]
        public async Task<IActionResult> GetReviewById([FromRoute] string id)
        {
            var query = new GetReviewByIdQuery(id);
            var reviewResult = await _mediator.Send(query);
            #region Switch Statement
            return reviewResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(reviewResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = reviewResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(reviewResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion
        }
        #endregion

        #region DeleteReview
        [HttpDelete("DeleteReview/{id}")]

        public async Task<IActionResult> DeleteReview([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeleteReviewCommand(id);
            var deleteReviewResult = await _mediator.Send(command);
            #region Switch Statement
            return deleteReviewResult switch
            {
                { IsSuccess: true, Data: true } => NoContent(),
                { IsSuccess: true, Message: not null } => new JsonResult(new { Message = deleteReviewResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(deleteReviewResult.Errors),
                _ => BadRequest("Invalid Fields for Delete review")
            };

            #endregion
        }
        #endregion

        #region UpdateReview
        [HttpPatch("UpdateReview/{id}")]

        public async Task<IActionResult> UpdateReview([FromRoute] string id, [FromBody] UpdateReviewRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand(id);
            var updateReviewResult = await _mediator.Send(command);
            #region Switch Statement
            return updateReviewResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updateReviewResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updateReviewResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updateReviewResult.Errors),
                _ => BadRequest("Invalid Fields for Update Review")
            };

            #endregion


        }
        #endregion

        #region ReviewByBeauticianId
        [HttpGet("GetReview/{beauticianId}")]
        public async Task<IActionResult> GetReviewByBeauticianId([FromRoute] string beauticianId)
        {
            var query = new GetReviewByBeauticianIdQuery(beauticianId);
            var reviewResult = await _mediator.Send(query);
            #region Switch Statement
            return reviewResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(reviewResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = reviewResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(reviewResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region ReviewByProductId
        [HttpGet("Get-Review/{productId}")]
        public async Task<IActionResult> GetReviewByProductId([FromRoute] string productId)
        {
            var query = new GetReviewByProductIdQuery(productId);
            var reviewResult = await _mediator.Send(query);
            #region Switch Statement
            return reviewResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(reviewResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = reviewResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(reviewResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion
        #endregion


    }
}

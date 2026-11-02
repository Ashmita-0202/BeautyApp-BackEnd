using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NV.Booking.Application.Booking.Command.AddBooking;
using NV.Booking.Application.Booking.Command.AddBooking.RequestMapper;
using NV.Booking.Application.Booking.Command.DeleteBooking;
using NV.Booking.Application.Booking.Command.UpdateBooking;
using NV.Booking.Application.Booking.Command.UpdateBooking.RequestCommandMapper;
using NV.Booking.Application.Booking.Queries.GetAllBooking;
using NV.Booking.Application.Booking.Queries.GetBookingByBeauticianId;
using NV.Booking.Application.Booking.Queries.GetBookingById;
using NV.Feedbacks.Application.Feedbacks.Queries.GetReviewByBeauticianId;
using TN.Authentication.Application.ServiceInterface;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Web.Controllers;

namespace NV.Web.Area.Bookings.Controllers
{
    public class BookingsControllers : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;

        public BookingsControllers(IMediator mediator, IUserServices userServices, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(userManager, roleManager)
        {
            _mediator = mediator;
            _userServices = userServices;
        }

        #region Bookings

        #region AddBookings
        [HttpPost("AddBookings")]

        public async Task<IActionResult> AddBookings([FromBody] AddBookingRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addBookingResult = await _mediator.Send(command);
            #region Switch Statement
            return addBookingResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddBookings), addBookingResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = addBookingResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addBookingResult.Errors),
                _ => BadRequest("Invalid Fields for Add Booking ")
            };

            #endregion
        }

        #endregion
        #region AllBooking
        [HttpGet("all-booking")]
        public async Task<IActionResult> GetAllBooking([FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetAllBookingByQuery(paginationRequest);
            var bookingResult = await _mediator.Send(query);
            #region Switch Statement
            return bookingResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(bookingResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = bookingResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(bookingResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }

        #endregion

        #region BookingById
        [HttpGet("Booking/{id}")]
        public async Task<IActionResult> GetBookingById([FromRoute] string id)
        {
            var query = new GetBookingByIdQuery(id);
            var bookingResult = await _mediator.Send(query);
            #region Switch Statement
            return bookingResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(bookingResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = bookingResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(bookingResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion
        }
        #endregion

        #region BookingByBeauticianId
        [HttpGet("GetBooking/{beauticianId}")]
        public async Task<IActionResult> GetBookingByBeauticianId([FromRoute] string beauticianId)
        {
            var query = new GetBookingByBeauticianIdQuery(beauticianId);
            var bookingResult = await _mediator.Send(query);
            #region Switch Statement
            return bookingResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(bookingResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = bookingResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(bookingResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region DeleteBooking
        [HttpDelete("DeleteBooking/{id}")]

        public async Task<IActionResult> DeleteBooking([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeleteBookingComand(id);
            var deleteBookingResult = await _mediator.Send(command);
            #region Switch Statement
            return deleteBookingResult switch
            {
                { IsSuccess: true, Data: true } => NoContent(),
                { IsSuccess: true, Message: not null } => new JsonResult(new { Message = deleteBookingResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(deleteBookingResult.Errors),
                _ => BadRequest("Invalid Fields for Delete Booking")
            };

            #endregion
        }
        #endregion

        #region UpdateBooking
        [HttpPatch("UpdateBooking/{id}")]

        public async Task<IActionResult> UpdateBooking([FromRoute] string id, [FromBody] UpdateBookingRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand(id);
            var updateBookingResult = await _mediator.Send(command);
            #region Switch Statement
            return updateBookingResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updateBookingResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updateBookingResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updateBookingResult.Errors),
                _ => BadRequest("Invalid Fields for Update Booking")
            };

            #endregion


        }
        #endregion


        #endregion
    }
}

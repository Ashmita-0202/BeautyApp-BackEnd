using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.AddBeautician.RequestCommandMapper;
using NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians;
using NV.Beautician.Application.Beautician.Commands.AddBeautiServiceToBeauticians.RequestCommandMapper;
using NV.Beautician.Application.Beautician.Commands.AddBeautyServices;
using NV.Beautician.Application.Beautician.Commands.AddBeautyServices.RequestCommandMapper;
using NV.Beautician.Application.Beautician.Commands.DeleteBeautician;
using NV.Beautician.Application.Beautician.Commands.DeleteBeautyServices;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician.RequestCommandMapper;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautyService;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautyService.RequestCommandMapper;
using NV.Beautician.Application.Beautician.Queries.AllBeautyServices;
using NV.Beautician.Application.Beautician.Queries.BeautyServiceById;
using NV.Beautician.Application.Beautician.Queries.GetAllBeautician;
using NV.Beautician.Application.Beautician.Queries.GetBeauticianById;
using TN.Authentication.Application.Authentication.Commands.AddPermissionToRoles;
using TN.Authentication.Application.ServiceInterface;
using TN.Authentication.Domain.Entities;
using TN.Shared.Domain.ExtensionMethod.Pagination;
using TN.Web.Controllers;

namespace NV.Web.Area.Beautician.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeauticianController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserServices _userServices;

        public BeauticianController(IMediator mediator,IUserServices userServices, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(userManager, roleManager) 
        {
            _mediator=mediator;
            _userServices=userServices;
        }

        #region AllBeautician
        [HttpGet("all-beautician")]
        public async Task<IActionResult> AllBeautician([FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetAllBeauticianByQuery(paginationRequest);
            var beauticianResult = await _mediator.Send(query);
            #region Switch Statement
            return beauticianResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(beauticianResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = beauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(beauticianResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region AddBeauticians
        [HttpPost("AddBeauticians")]
        public async Task<IActionResult> AddBeauticians([FromBody] AddBeauticianRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addBeauticianResult = await _mediator.Send(command);
            #region Switch Statement
            return addBeauticianResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddBeauticians), addBeauticianResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = addBeauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addBeauticianResult.Errors),
                _ => BadRequest("Invalid Fields for Add Beauticians ")
            };

            #endregion
        }

        #endregion

        #region BeauticiansById
        [HttpGet("Beauticians/{id}")]
        public async Task<IActionResult> GetBeauticiansById([FromRoute] string id)
        {
            var query = new GetBeauticianByIdQuery(id);
            var beauticianResult = await _mediator.Send(query);
            #region Switch Statement
            return beauticianResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(beauticianResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = beauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(beauticianResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion
        }
        #endregion

        #region UpdateBeautician
        [HttpPatch("UpdateBeautician/{id}")]

        public async Task<IActionResult> UpdateBeautician([FromRoute] string id, [FromBody] UpdateBeauticianRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand(id);
            var updateBeauticianResult = await _mediator.Send(command);
            #region Switch Statement
            return updateBeauticianResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updateBeauticianResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updateBeauticianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updateBeauticianResult.Errors),
                _ => BadRequest("Invalid Fields for Assign Beauticians")
            };

            #endregion

        }
        #endregion

        #region DeleteBeauticians
        [HttpDelete("DeleteBeautcian/{id}")]

        public async Task<IActionResult> DeleteBEautican([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeleteBeauticianCommand(id);
            var deleteBeautcianResult = await _mediator.Send(command);
            #region Switch Statement
            return deleteBeautcianResult switch
            {
                { IsSuccess: true, Data: true } => NoContent(),
                { IsSuccess: true, Message: not null } => new JsonResult(new { Message = deleteBeautcianResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(deleteBeautcianResult.Errors),
                _ => BadRequest("Invalid Fields for Delete Beauticians")
            };

            #endregion
        }
        #endregion

        #region AddBeautyServices
        [HttpPost("AddBeautyServices")]
        public async Task<IActionResult> AddBeautyServices([FromBody] AddBeautyServicesRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addBeautyServicesResult = await _mediator.Send(command);
            #region Switch Statement
            return addBeautyServicesResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddBeautyServices), addBeautyServicesResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = addBeautyServicesResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addBeautyServicesResult.Errors),
                _ => BadRequest("Invalid Fields for Add BeautyServices ")
            };

            #endregion
        }

        #endregion

        #region AllBeautician
        [HttpGet("All-BeautyService")]
        public async Task<IActionResult> AllBeauytService([FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetAllBeautyServiceByQuery(paginationRequest);
            var beautyResult = await _mediator.Send(query);
            #region Switch Statement
            return beautyResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(beautyResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = beautyResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(beautyResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion

        }
        #endregion

        #region BeautyServiceById
        [HttpGet("BeautyService/{id}")]
        public async Task<IActionResult> GetBeautyServiceById([FromRoute] string id)
        {
            var query = new GetBeautyServiceByIdQuery(id);
            var beautyServiceResult = await _mediator.Send(query);
            #region Switch Statement
            return beautyServiceResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(beautyServiceResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = beautyServiceResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(beautyServiceResult.Errors),
                _ => BadRequest("Invalid page and pageSize Fields")
            };
            #endregion
        }
        #endregion

        #region UpdateBeautyService
        [HttpPatch("UpdateBeautyService/{id}")]

        public async Task<IActionResult> UpdateBeautyService([FromRoute] string id, [FromBody] UpdateBeautyServiceRequest request)
        {
            //Mapping command and request
            var command = request.Tocommand(id);
            var updateBeautyServiceResult = await _mediator.Send(command);
            #region Switch Statement
            return updateBeautyServiceResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updateBeautyServiceResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updateBeautyServiceResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updateBeautyServiceResult.Errors),
                _ => BadRequest("Invalid Fields for Assign BeautyServices")
            };

            #endregion

        }
        #endregion


        #region DeleteBeautyService
        [HttpDelete("DeleteBeautyService/{id}")]

        public async Task<IActionResult> DeleteBeautyService([FromRoute] string id, CancellationToken cancellationToken)
        {
            var command = new DeleteBeautyServiceCommand(id);
            var deleteBeautyServiceResult = await _mediator.Send(command);
            #region Switch Statement
            return deleteBeautyServiceResult switch
            {
                { IsSuccess: true, Data: true } => NoContent(),
                { IsSuccess: true, Message: not null } => new JsonResult(new { Message = deleteBeautyServiceResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(deleteBeautyServiceResult.Errors),
                _ => BadRequest("Invalid Fields for Delete beauty Service")
            };

            #endregion
        }
        #endregion

        #region AddServicesToBeautician
        [HttpPost("AddServicesToBeautician")]
        public async Task<IActionResult> AddServicesToBeautician([FromBody] AddServicesToBeauticianRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand();
            var result = await _mediator.Send(command);

            #region switch
            return result switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(result.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = result.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Invalid beauticianId or beautiServiceId")

            };

            #endregion
        }

        #endregion

    }
}

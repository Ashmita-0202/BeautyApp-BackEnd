using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TN.Authentication.Application.Authentication.Queries.GetExpiredDateItemStatusByCompany;
using TN.Authentication.Domain.Entities;
using TN.Shared.Application.Shared.Command.UpdateExpiredDateItemStatusByCompany.RequestCommandMapper;
using TN.Shared.Application.Shared.Command.UpdateItemStatusByCompany;
using TN.Web.Controllers;

namespace TN.Web.Area.Settings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : BaseController
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(userManager, roleManager)
        {
            _mediator = mediator;

        }

        #region GetItemStatusByCompanyId
        [HttpGet("GetItemStatusByCompany/{id}")]
        public async Task<IActionResult> GetItemStatusByCompany([FromRoute] string id)
        {
            var query = new GetItemStatusByCompanyQuery(id);
            var expiredTimeItemStatus = await _mediator.Send(query);
            #region Switch Statement
            return expiredTimeItemStatus switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(expiredTimeItemStatus.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = expiredTimeItemStatus.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(expiredTimeItemStatus.Errors),
                _ => BadRequest("Invalid companyId Fields")
            };
            #endregion

        }
        #endregion

        #region UpdateItemStatusByCompany
        [HttpPatch("UpdateItemStatusByCompany/{id}")]

        public async Task<IActionResult> UpdateStatusByCompany([FromRoute] string id, [FromBody] UpdateItemStatusByCompanyRequest request)
        {
            //Mapping command and request
            var command = request.ToCommand(id);
            var updateExpiredDateItemStatus = await _mediator.Send(command);
            #region Switch Statement
            return updateExpiredDateItemStatus switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(updateExpiredDateItemStatus.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = updateExpiredDateItemStatus.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(updateExpiredDateItemStatus.Errors),
                _ => BadRequest("Invalid Fields for Update ExpiredDateItem status")
            };

            #endregion


        }
        #endregion
    }
}

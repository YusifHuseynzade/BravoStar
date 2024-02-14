using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Queries.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Asan_LMS.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserRoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAppUserRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(new { Message = "AppUserRole creation failed." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAppUserRoleQueryRequest request)
        {
            var appuserroles = await _mediator.Send(request);

            return Ok(appuserroles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var requestModel = new GetByIdAppUserRoleQueryRequest { Id = id };
            var appuserrole = await _mediator.Send(requestModel);

            return appuserrole != null
                ? (IActionResult)Ok(appuserrole)
                : NotFound(new { Message = "AppUserRole not found." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateAppUserRoleCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(new { Message = "AppUserRole updated successfully." });
            }

            return BadRequest(new { Message = "AppUserRole update failed." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var requestModel = new DeleteAppUserRoleCommandRequest { Id = id };
            var response = await _mediator.Send(requestModel);

            return response.IsSuccess
                ? (IActionResult)Ok(new { Message = "AppUserRole deleted successfully." })
                : NotFound(new { Message = "AppUserRole not found." });
        }
    }
}
using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using ApplicationUserDetails.Queries.Request;
using ApplicationUserDetails.Querires.Request;
using ApplicationUserDetails.Querires.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BravoStar.AdminPanel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApplicationUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminApplicationUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllAppUserQueryRequest request)
        {
            var appUsers = await _mediator.Send(request);

            return Ok(appUsers);
        }

        [HttpGet("appuservoting")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAppUserVoting([FromQuery] GetAllAppUserVotingQueryRequest request)
        {
            var appUsers = await _mediator.Send(request);

            return Ok(appUsers);
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,AppUser")]
        public async Task<ActionResult<GetByIdAdminUserQueryResponse>> GetUserById(int id)
        {
            var requestModel = new GetByIdAdminUserQueryRequest { Id = id };
            var user = await _mediator.Send(requestModel);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost("CreateUser")]
        //[Authorize(Roles = "Admin,AppUser")]
        public async Task<ActionResult<CreateAppUserCommandResponse>> CreateUser([FromQuery] CreateAppUserCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            if (response.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(response.Message);

            }
        }

        [HttpPut]
        //[Authorize(Roles = "Admin,AppUser")]
        public async Task<IActionResult> UpdateUser(UpdateAppUserCommandRequest requestModel)
        {
            try
            {
                var response = await _mediator.Send(requestModel);
                if (response.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return NoContent();
        }

        [HttpPut("ChangePassword")]
        //[Authorize(Roles = "Admin,AppUser")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommandRequest requestModel)
        {
            try
            {
                var response = await _mediator.Send(requestModel);
                if (response.IsSuccess)
                {
                    return Ok(response.Message);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DeleteAppUserCommandResponse>> DeleteUser(int id)
        {
            var requestModel = new DeleteAppUserCommandRequest { Id = id };
            var response = await _mediator.Send(requestModel);
            if (response == null)
            {
                return NotFound();
            }
            return response;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginAppUserCommandResponse>> Login(LoginAppUserCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}


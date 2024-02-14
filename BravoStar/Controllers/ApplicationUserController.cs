﻿using ApplicationUserDetails.Commands.Request;
using ApplicationUserDetails.Commands.Response;
using ApplicationUserDetails.Querires.Request;
using ApplicationUserDetails.Querires.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Asan_LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationUserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateUser")]
        //[Authorize(Roles = "Admin,AppUser")]
        public async Task<ActionResult<CreateAppUserCommandResponse>> CreateUser(CreateAppUserCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            if (response.IsSuccess)
            {
                return Ok(new
                {
                    response.IsSuccess
                });
            }
            else
            {
                throw new Exception(response.Message);
            }
        }


        [HttpPut("ChangePassword")]
        public async Task<ActionResult<ChangePasswordCommandResponse>> ChangePassword(ChangePasswordCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            if (response.IsSuccess)
            {
                return Ok(new
                {
                    response.IsSuccess,
                    response.Message
                });
            }
            else
            {
                throw new Exception(response.Message);
            }
        }

        [HttpPut("UpdateUser")]
        //[Authorize(Roles = "Admin,AppUser")]
        public async Task<IActionResult> UpdateUser(UpdateAppUserCommandRequest requestModel)
        {
            try
            {
                var response = await _mediator.Send(requestModel);
                return Ok(response.IsSuccess);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }



        [HttpPost("Login")]
        public async Task<ActionResult<LoginAppUserCommandResponse>> Login(LoginAppUserCommandRequest requestModel)
        {
            var response = await _mediator.Send(requestModel);
            if (response.IsSuccess)
            {
                return Ok(
                    new
                    {
                        response.IsSuccess,
                        response.JwtToken,
                        response.RefreshToken,
                        response.UserId
                    }
                    );
            }
            else
            {
                throw new Exception(response.Message);
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<GetByIdAppUserQueryResponse>> GetUserById(int id)
        {
            var requestModel = new GetByIdAppUserQueryRequest { Id = id };
            var user = await _mediator.Send(requestModel);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}

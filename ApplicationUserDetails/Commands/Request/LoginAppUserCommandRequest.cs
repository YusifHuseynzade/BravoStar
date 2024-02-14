using ApplicationUserDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.Commands.Request
{
    public class LoginAppUserCommandRequest : IRequest<LoginAppUserCommandResponse>
    {
        public string Badge { get; set; }
        public string Password { get; set; }
    }
}


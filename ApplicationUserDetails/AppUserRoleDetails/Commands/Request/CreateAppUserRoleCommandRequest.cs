using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Commands.Request
{
    public class CreateAppUserRoleCommandRequest : IRequest<CreateAppUserRoleCommandResponse>
    {
        public string RoleName { get; set; }
    }
}
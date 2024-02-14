using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Commands.Request
{
    public class UpdateAppUserRoleCommandRequest : IRequest<UpdateAppUserRoleCommandResponse>
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
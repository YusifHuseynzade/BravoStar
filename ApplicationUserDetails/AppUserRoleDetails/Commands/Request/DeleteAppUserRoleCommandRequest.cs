using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Commands.Request
{
    public class DeleteAppUserRoleCommandRequest : IRequest<DeleteAppUserRoleCommandResponse>
    {
        public int Id { get; set; }
    }
}
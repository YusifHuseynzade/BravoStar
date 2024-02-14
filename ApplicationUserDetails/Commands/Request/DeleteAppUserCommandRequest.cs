using ApplicationUserDetails.Commands.Response;
using MediatR;

namespace ApplicationUserDetails.Commands.Request
{
    public class DeleteAppUserCommandRequest : IRequest<DeleteAppUserCommandResponse>
    {
        public int Id { get; set; }
    }
}

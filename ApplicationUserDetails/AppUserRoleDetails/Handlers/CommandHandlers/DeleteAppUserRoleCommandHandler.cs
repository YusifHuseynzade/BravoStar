using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.CommandHandlers
{
    public class DeleteAppUserRoleCommandHandler : IRequestHandler<DeleteAppUserRoleCommandRequest, DeleteAppUserRoleCommandResponse>
    {
        private readonly IAppUserRoleRepository _repository;

        public DeleteAppUserRoleCommandHandler(IAppUserRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteAppUserRoleCommandResponse> Handle(DeleteAppUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var appuserrole = await _repository.GetAsync(x => x.Id == request.Id);

            if (appuserrole == null)
            {
                return new DeleteAppUserRoleCommandResponse { IsSuccess = false };
            }

            _repository.Remove(appuserrole);
            await _repository.CommitAsync();

            return new DeleteAppUserRoleCommandResponse
            {
                IsSuccess = true
            };
        }
    }
}
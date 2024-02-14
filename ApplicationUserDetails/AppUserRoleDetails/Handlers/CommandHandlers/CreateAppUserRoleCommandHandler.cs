using ApplicationUserDetails.AppUserRoleDetails.Commands.Request;
using ApplicationUserDetails.AppUserRoleDetails.Commands.Response;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationUserDetails.AppUserRoleDetails.Handlers.CommandHandlers
{
    public class CreateAppUserRoleCommandHandler : IRequestHandler<CreateAppUserRoleCommandRequest, CreateAppUserRoleCommandResponse>
    {
        private readonly IAppUserRoleRepository _repository;
        private readonly IMapper _mapper;

        public CreateAppUserRoleCommandHandler(IAppUserRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateAppUserRoleCommandResponse> Handle(CreateAppUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var appuserrole = _mapper.Map<AppUserRole>(request);

            await _repository.AddAsync(appuserrole);
            await _repository.CommitAsync();

            return new CreateAppUserRoleCommandResponse
            {
                IsSuccess = true,
            };
        }
    }
}
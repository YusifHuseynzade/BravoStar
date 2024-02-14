using AppUserNominationDetails.Commands.Request;
using AppUserNominationDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace AppUserNominationDetails.Handlers.CommandHandlers;

public class DeleteAppUserVotingCommandHandler : IRequestHandler<DeleteAppUserVotingCommandRequest, DeleteAppUserVotingCommandResponse>
{
    private readonly IAppUserRepository _repository;
    private readonly IAppUserNominationRepository _appUserNominationRepository;
    private readonly INominationRepository _nominationRepository;


    public DeleteAppUserVotingCommandHandler(IAppUserRepository repository, IAppUserNominationRepository appUserNominationRepository, INominationRepository nominationRepository)
    {
        _repository = repository;
        _appUserNominationRepository = appUserNominationRepository;
        _nominationRepository = nominationRepository;
    }

    public async Task<DeleteAppUserVotingCommandResponse> Handle(DeleteAppUserVotingCommandRequest request, CancellationToken cancellationToken)
    {
        var Project = await _repository.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (Project == null)
        {
            return new DeleteAppUserVotingCommandResponse { IsSuccess = false };
        }

        _repository.Remove(Project);
        await _repository.CommitAsync();

        return new DeleteAppUserVotingCommandResponse
        {
            IsSuccess = true
        };
    }
}

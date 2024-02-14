using AppUserNominationDetails.Commands.Request;
using AppUserNominationDetails.Commands.Response;
using Domain.IRepositories;
using MediatR;

namespace AppUserNominationDetails.Handlers.CommandHandlers;

//public class CreateAppUserVotingCommandHandler : IRequestHandler<CreateAppUserVotingCommandRequest, CreateAppUserVotingCommandResponse>
//{
//    private readonly IAppUserRepository _repository;
//    private readonly IAppUserNominationRepository _appUserNominationRepository;
//    private readonly INominationRepository _nominationRepository;


//    public CreateAppUserVotingCommandHandler(IAppUserRepository repository, IAppUserNominationRepository appUserNominationRepository, INominationRepository nominationRepository)
//    {
//        _repository = repository;
//        _appUserNominationRepository = appUserNominationRepository;
//        _nominationRepository = nominationRepository;
//    }
//    //public async Task<CreateAppUserVotingCommandResponse> Handle(CreateAppUserVotingCommandRequest request, CancellationToken cancellationToken)
//    //{

//    //}
//}

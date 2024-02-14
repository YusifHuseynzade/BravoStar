using AppUserNominationDetails.Commands.Response;
using MediatR;

namespace AppUserNominationDetails.Commands.Request;

public class CreateAppUserVotingCommandRequest : IRequest<CreateAppUserVotingCommandResponse>
{
    public int AppUserId { get; set; }
    public List<CreateAppUserNominationCommanRequest> AppUserNominations { get; set; }

}

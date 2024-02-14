using AppUserNominationDetails.Commands.Response;
using MediatR;

namespace AppUserNominationDetails.Commands.Request;

public class DeleteAppUserVotingCommandRequest : IRequest<DeleteAppUserVotingCommandResponse>
{
    public int Id { get; set; }
}

using AppUserNominationDetails.Commands.Response;
using MediatR;

namespace AppUserNominationDetails.Commands.Request;

public class CreateAppUserVotingCommandRequest : IRequest<CreateAppUserVotingCommandResponse>
{
    public int VoiceSenderUserId { get; set; }
   public List<CreatedSelectedUserCommandRequest> SelectedUsers { get; set; }
}

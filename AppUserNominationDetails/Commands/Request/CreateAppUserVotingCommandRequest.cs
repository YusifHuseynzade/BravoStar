using AppUserNominationDetails.Commands.Response;
using MediatR;

namespace AppUserNominationDetails.Commands.Request;

public class CreateAppUserVotingCommandRequest : IRequest<CreateAppUserVotingCommandResponse>
{
    public int VoiceSenderUserId { get; set; }
    public int SelectedUserId { get; set; }
    public int NominationId { get; set; }
    public float Rate { get; set; }
}

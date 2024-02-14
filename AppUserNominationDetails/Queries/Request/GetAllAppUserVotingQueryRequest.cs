using AppUserNominationDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace AppUserNominationDetails.Queries.Request;

public class GetAllAppUserVotingQueryRequest : IRequest<List<GetAppUserVotingListResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

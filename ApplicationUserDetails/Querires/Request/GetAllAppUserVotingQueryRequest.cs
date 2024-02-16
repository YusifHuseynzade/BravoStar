using ApplicationUserDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace ApplicationUserDetails.Queries.Request;

public class GetAllAppUserVotingQueryRequest : IRequest<List<GetAllAppUserVotingQueryResponse>>
{
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
}

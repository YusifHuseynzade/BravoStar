using ApplicationUserDetails.Queries.Response;
using Common.Constants;
using MediatR;

namespace ApplicationUserDetails.Queries.Request;

public class GetAllAppUserVotingQueryRequest : IRequest<List<GetAppUserVotingListResponse>>
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Page { get; set; } = 1;
    public ShowMoreDto? ShowMore { get; set; }
    public void NormalizeDates()
    {
        StartDate = StartDate?.Date.ToUniversalTime();
        EndDate = EndDate?.Date.ToUniversalTime();
    }
}

using ApplicationUserDetails.Querires.Response;
using Common.Constants;
using MediatR;

namespace ApplicationUserDetails.Querires.Request
{
    public class GetByIdAppUserQueryRequest : IRequest<List<GetByAppUserIdVotedAppUserListResponse>>
    {
        public int Id { get; set; }
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
}

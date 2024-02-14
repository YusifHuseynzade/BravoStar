using ApplicationUserDetails.Querires.Response;
using Common.Constants;
using MediatR;

namespace ApplicationUserDetails.Querires.Request
{
    public class GetAllAppUserQueryRequest : IRequest<List<GetAppUserListResponse>>
    {
        public int Page { get; set; } = 1;
        public ShowMoreDto? ShowMore { get; set; }
    }
}

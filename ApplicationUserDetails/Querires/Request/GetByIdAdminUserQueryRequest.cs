using ApplicationUserDetails.Querires.Response;
using MediatR;

namespace ApplicationUserDetails.Querires.Request
{
    public class GetByIdAdminUserQueryRequest : IRequest<GetByIdAdminUserQueryResponse>
    {
        public int Id { get; set; }
    }
}

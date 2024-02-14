using ApplicationUserDetails.Querires.Response;
using MediatR;

namespace ApplicationUserDetails.Querires.Request
{
    public class GetByIdAppUserQueryRequest : IRequest<GetByIdAppUserQueryResponse>
    {
        public int Id { get; set; }
    }
}

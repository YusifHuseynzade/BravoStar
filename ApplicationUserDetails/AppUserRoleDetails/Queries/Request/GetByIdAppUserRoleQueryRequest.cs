using ApplicationUserDetails.AppUserRoleDetails.Queries.Response;
using MediatR;

namespace ApplicationUserDetails.AppUserRoleDetails.Queries.Request
{
    public class GetByIdAppUserRoleQueryRequest : IRequest<GetByIdAppUserRoleQueryResponse>
    {
        public int Id { get; set; }
    }
}
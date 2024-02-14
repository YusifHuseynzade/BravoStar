using ApplicationUserDetails.AppUserRoleDetails.Queries.Response;
using MediatR;
using System.Collections.Generic;

namespace ApplicationUserDetails.AppUserRoleDetails.Queries.Request
{
    public class GetAllAppUserRoleQueryRequest : IRequest<List<GetAllAppUserRoleQueryResponse>>
    {
    }
}
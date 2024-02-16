using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Querires.Response
{
    public class GetByAppUserIdVotedAppUserListResponse
    {
        public int TotalVotedAppUserCount { get; set; }
        public List<GetByIdAppUserQueryResponse> AppUsers { get; set; }
    }
}

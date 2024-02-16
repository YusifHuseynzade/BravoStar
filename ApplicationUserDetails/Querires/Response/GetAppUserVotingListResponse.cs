using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Queries.Response
{
    public class GetAppUserVotingListResponse
    {
        public int TotalAppUserVotingCount { get; set; }
        public List<GetAllAppUserVotingQueryResponse> AppUserVotes { get; set; }
    }
}

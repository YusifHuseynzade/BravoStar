using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Querires.Response
{
    public class GetAppUserListResponse
    {
        public int TotalAppUserCount { get; set; }
        public List<GetAllAppUserQueryResponse> AppUsers { get; set; }
    }
}

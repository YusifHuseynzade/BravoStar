using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUserDetails.Querires.Response
{
    public class GetAppUserRateResponse
    {
        public int Id { get; set; }
        public float Rate { get; set; }
        public float Coefficient { get; set; }
        public float Result { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUserNominationDetails.Commands.Request
{
    public class CreateNominationForAppUserCommandRequest
    {
        public int NominationId { get; set; }
        public float Rate { get; set; }
    }
}

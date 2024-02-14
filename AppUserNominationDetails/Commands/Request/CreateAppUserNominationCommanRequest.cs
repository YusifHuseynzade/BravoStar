using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUserNominationDetails.Commands.Request
{
    public class CreateAppUserNominationCommanRequest
    {
        public int SameProjectUserId { get; set; }
        public CreateNominationForAppUserCommandRequest Nomination { get; set; }
    }
}

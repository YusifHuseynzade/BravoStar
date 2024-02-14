using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Nomination : BaseEntity
    {
        public string Name { get; set; }
        public List<AppUserNomination> AppUserNominations { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}

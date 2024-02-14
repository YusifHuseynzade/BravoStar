using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public void SetDetail(string name)
        {
            this.Name = name;
        }
    }
}

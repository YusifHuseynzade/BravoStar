﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppUserRole : BaseEntity
    {
        public int AppUserId { get; set; }
        public int RoleId { get; set; }
        public AppUser AppUser { get; set; }
        public Role Role { get; set; }
    }
}

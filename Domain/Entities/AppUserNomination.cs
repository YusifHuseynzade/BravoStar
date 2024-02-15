using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AppUserNomination : BaseEntity
    {
        public int AppUserId { get; set; }
        public int NomineeId { get; set; }
        public int NominationId { get; set; }
        public AppUser AppUser { get; set; }
        public Nomination Nomination { get; set; }
        public float Rate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public void SetDetail(float rate)
        {
            this.Rate = rate;
        }
    }
}

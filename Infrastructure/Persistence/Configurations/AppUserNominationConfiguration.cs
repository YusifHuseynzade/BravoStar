using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class AppUserNominationConfiguration : IEntityTypeConfiguration<AppUserNomination>
    {
        public void Configure(EntityTypeBuilder<AppUserNomination> builder)
        {

            builder.HasOne(ur => ur.AppUser)
                   .WithMany(u => u.AppUserNominations)
                   .HasForeignKey(ur => ur.AppUserId);

            builder.HasOne(ur => ur.Nomination)
                   .WithMany(r => r.AppUserNominations)
                   .HasForeignKey(ur => ur.NominationId);
        }
    }
}

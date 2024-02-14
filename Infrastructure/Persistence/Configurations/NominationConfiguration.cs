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
    public class NominationConfiguration : IEntityTypeConfiguration<Nomination>
    {
        public void Configure(EntityTypeBuilder<Nomination> builder)
        {
            builder.HasMany(u => u.AppUserNominations)
                 .WithOne(ur => ur.Nomination)
                 .HasForeignKey(ur => ur.NominationId);

            builder.Property(r => r.Name)
                .IsRequired();
        }
    }
}

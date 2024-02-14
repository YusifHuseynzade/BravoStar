using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {

            builder.HasOne(ur => ur.AppUser)
                   .WithMany(u => u.AppUserRoles)
                   .HasForeignKey(ur => ur.AppUserId);

            builder.HasOne(ur => ur.Role)
                   .WithMany(r => r.AppUserRoles)
                   .HasForeignKey(ur => ur.RoleId);
        }
    }
}

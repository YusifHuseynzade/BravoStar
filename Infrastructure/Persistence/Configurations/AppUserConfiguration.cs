using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // İlişkileri belirtmek
            builder.HasMany(u => u.AppUserRoles)
                   .WithOne(ur => ur.AppUser)
                   .HasForeignKey(ur => ur.AppUserId);

            // İlişkileri belirtmek
            builder.HasMany(u => u.AppUserNominations)
                   .WithOne(ur => ur.AppUser)
                   .HasForeignKey(ur => ur.AppUserId);

            builder.HasOne(u => u.Project)
                     .WithMany(p => p.AppUsers)
                   .HasForeignKey(u => u.ProjectId);

            // Özellikleri belirtmek
            builder.Property(u => u.FullName)
                   .IsRequired();

            builder.Property(u => u.Badge)
                   .IsRequired();

            builder.Property(u => u.Password)
                   .IsRequired();

        
        }
    }
}
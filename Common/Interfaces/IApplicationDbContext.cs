using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Nomination> Nominations { get; set; }
        public DbSet<AppUserNomination> AppUserNominations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
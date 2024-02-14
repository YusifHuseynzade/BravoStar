using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly AppDbContext _context;

        public AppUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByIdAsync(int userId)
        {
            return await _context.AppUsers.FindAsync(userId);
        }
    }
}

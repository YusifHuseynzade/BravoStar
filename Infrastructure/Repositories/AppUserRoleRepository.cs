using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class AppUserRoleRepository : Repository<AppUserRole>, IAppUserRoleRepository
    {
        private readonly AppDbContext _context;

        public AppUserRoleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<AppUserRole>> GetAllAsync(Expression<Func<AppUserRole, bool>> predicate)
        {
            return await _context.Set<AppUserRole>().Where(predicate).ToListAsync();
        }
        public async Task DeleteAsync(AppUserRole entity)
        {
            _context.Set<AppUserRole>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
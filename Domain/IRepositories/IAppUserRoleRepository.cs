using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IAppUserRoleRepository : IRepository<AppUserRole>
    {
        Task<List<AppUserRole>> GetAllAsync(Expression<Func<AppUserRole, bool>> predicate);
        Task DeleteAsync(AppUserRole entity);
    }
}
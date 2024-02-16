using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IRepository<TEntity>
    {
        Task AddAsync(TEntity entity);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        TEntity Get(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        void Remove(TEntity entity);
        int Commit();
        Task<int> CommitAsync();
        Task UpdateAsync(TEntity entity);
        IQueryable<TEntity> Include(IQueryable<TEntity> query, params string[] includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, params string[] includes);


    }
}

using Domain.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null) { foreach (var reff in includes) query = query.Include(reff); }

            return await query.FirstOrDefaultAsync(exp);
        }
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes != null) { foreach (var reff in includes) query = query.Include(reff); }
            return query.Where(exp);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null) { foreach (var reff in includes) query = query.Include(reff); }

            return await query.FirstOrDefaultAsync(exp);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                foreach (var reff in includes)
                {
                    query = query.Include(reff);
                }
            }

            return query.FirstOrDefault(exp);
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (includes != null) { foreach (var reff in includes) query = query.Include(reff); }

            return await query.AnyAsync(exp);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Include(IQueryable<TEntity> query, params string[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
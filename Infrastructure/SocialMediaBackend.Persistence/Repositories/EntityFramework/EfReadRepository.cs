using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Repositories;
using SocialMediaBackend.Domain.Entities.Base;
using System.Linq.Expressions;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework
{
    public class EfReadRepository<TEntity, TContext> : IReadRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {

        private readonly TContext _context;
        public EfReadRepository(TContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> GetAll(bool tracking = true)
        {
            IQueryable<TEntity> query = tracking ? _context.Set<TEntity>() : _context.Set<TEntity>().AsNoTracking();
            return query;
        }

        public async Task<TEntity> GetByIdAsync(string id, bool tracking = true)
        {
            if (!Guid.TryParse(id, out var guidId))
                throw new ArgumentException("Invalid GUID format", nameof(id));

            IQueryable<TEntity> query = tracking ? _context.Set<TEntity>() : _context.Set<TEntity>().AsNoTracking();
            TEntity? result = await query.FirstOrDefaultAsync(data => data.Id == guidId);

            if (result == null)
                throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} with ID {id} not found.");

            return result;
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            IQueryable<TEntity> query = tracking ? _context.Set<TEntity>() : _context.Set<TEntity>().AsNoTracking();
            TEntity? result = await query.FirstOrDefaultAsync(predicate);

            if(result == null)
                throw new InvalidOperationException($"Entity of type {typeof(TEntity).Name} matching the criteria not found.");

            return result;
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate, bool tracking = true)
        {
            IQueryable<TEntity> query = tracking ? 
                _context.Set<TEntity>().Where(predicate) : 
                _context.Set<TEntity>().Where(predicate).AsNoTracking();
            return query;
        }
    }
}

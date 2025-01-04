using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SocialMediaBackend.Application.Repositories;
using SocialMediaBackend.Domain.Entities.Base;

namespace SocialMediaBackend.Persistence.Repositories.EntityFramework
{
    public class EfWriteRepository<TEntity, TContext> : IWriteRepository<TEntity>
        where TEntity : Entity
        where TContext : DbContext
    {

        private readonly TContext _context;
        public EfWriteRepository(TContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await _context.Set<TEntity>().AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            return true;
        }

        public bool Delete(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = _context.Set<TEntity>().Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var guidId))
                throw new ArgumentException("Invalid GUID format", nameof(id));

            TEntity? entity = await _context.Set<TEntity>().FirstOrDefaultAsync(data => data.Id == guidId);

            if (entity == null)
                throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} with ID {id} was found.");

            return Delete(entity);
        }

        public async Task<bool> DeleteWhere(Expression<Func<TEntity, bool>> expression)
        {
            TEntity? entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (entity == null)
                throw new InvalidOperationException($"No entity of type {typeof(TEntity).Name} was found.");

            return Delete(entity);
        }

        public bool DeleteRange(List<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            return true;
        }

        public bool Update(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = _context.Set<TEntity>().Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

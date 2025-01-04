using System.Linq.Expressions;
using SocialMediaBackend.Domain.Entities.Base;

namespace SocialMediaBackend.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T: Entity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Delete(T entity);
        Task<bool> DeleteByIdAsync(string id);
        bool DeleteRange(List<T> entities);
        Task<bool> DeleteWhere(Expression<Func<T, bool>> expression);
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}

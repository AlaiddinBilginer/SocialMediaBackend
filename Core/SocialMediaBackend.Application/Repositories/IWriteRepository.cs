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
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}

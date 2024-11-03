using SocialMediaBackend.Domain.Entities.Base;
using System.Linq.Expressions;

namespace SocialMediaBackend.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : Entity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}

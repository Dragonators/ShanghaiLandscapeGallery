using System.Linq.Expressions;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;

namespace PM.Gallery.Domain.IRepository;

public interface IRepository<TEntity> where TEntity : class
{
    public ISpecificationTools<TEntity> UseSpecification();
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(Guid id);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task DeleteRangeAsync(IEnumerable<Guid> ids);
    bool Contains(Expression<Func<TEntity, bool>> predicate);
}
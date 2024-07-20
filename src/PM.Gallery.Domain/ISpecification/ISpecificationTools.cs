using System.Linq.Expressions;

namespace PM.Gallery.Domain.ISpecification;

public interface ISpecificationTools<T> where T : class
{
    public ISpecificationTools<T> Apply(ISpecification<T> specification);
    public ISpecificationTools<TResult> Select<TResult>(Expression<Func<T, TResult>> selector) where TResult : class;
    public ISpecificationTools<T> AsSplitQuery();
    public ISpecificationTools<T> AsNoTracking();
    public IAsyncEnumerable<T> AsAsyncEnumerable();
    public Task<T?> FirstOrDefaultAsync();
    public Task<List<T>> ToListAsync();
}
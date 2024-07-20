using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PM.Gallery.Domain.ISpecification;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications;

public class SpecificationTools<T> : ISpecificationTools<T> where T : class
{
    public SpecificationTools(IQueryable<T> query)
    {
        _specifications = new List<ISpecification<T>>();
        Query = query;
    }

    private readonly ICollection<ISpecification<T>> _specifications;
    private IQueryable<T> Query { get; set; }

    public ISpecificationTools<T> Apply(ISpecification<T> specification)
    {
        _specifications.Add(specification);
        return this;
    }

    public ISpecificationTools<TResult> Select<TResult>(Expression<Func<T, TResult>> selector) where TResult : class
    {
        return new SpecificationTools<TResult>(Query.Select(selector));
    }

    public ISpecificationTools<T> AsSplitQuery()
    {
        Query = Query.AsSplitQuery();
        return this;
    }

    public ISpecificationTools<T> AsNoTracking()
    {
        Query = Query.AsNoTracking();
        return this;
    }

    public IAsyncEnumerable<T> AsAsyncEnumerable()
    {
        ApplySpecification();
        return Query.AsAsyncEnumerable();
    }

    public async Task<T?> FirstOrDefaultAsync()
    {
        ApplySpecification();
        return await Query.FirstOrDefaultAsync();
    }

    public async Task<List<T>> ToListAsync()
    {
        ApplySpecification();
        return await Query.ToListAsync();
    }

    private void ApplySpecification()
    {
        if (Query is null) throw new NullReferenceException("Query cannot be null");
        foreach (var specification in _specifications)
            Query = specification.Type switch
            {
                (int)OperationType.Where => specification.Expressions.Aggregate(Query!, (current, expression) =>
                    current.Where((expression as Expression<Func<T, bool>>)!)),

                (int)OperationType.Include => specification.Expressions.Aggregate(Query!, (current, expression) =>
                    current.Include((expression as Expression<Func<T, object>>)!)),

                (int)OperationType.OrderBy => specification.Expressions.Aggregate(Query!, (current, expression) =>
                    current.OrderBy((expression as Expression<Func<T, object>>)!)),

                (int)OperationType.OrderByDescending => specification.Expressions.Aggregate(Query!,
                    (current, expression) =>
                        current.OrderByDescending((expression as Expression<Func<T, object>>)!)),

                (int)OperationType.ThenBy => specification.Expressions.Aggregate((Query as IOrderedQueryable<T>)!,
                    (current, expression) =>
                        current.ThenBy((expression as Expression<Func<T, object>>)!)),

                (int)OperationType.ThenByDescending => specification.Expressions.Aggregate(
                    (Query as IOrderedQueryable<T>)!, (current, expression) =>
                        current.ThenByDescending((expression as Expression<Func<T, object>>)!)),

                (int)OperationType.Paging => Query.Skip(specification.Skip).Take(specification.Take),

                _ => throw new ArgumentOutOfRangeException()
            };
    }
}
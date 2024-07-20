using System.Linq.Expressions;
using PM.Gallery.Domain.ISpecification;

namespace PM.Gallery.Infrastructure.Specifications;

public abstract class BaseSpecification<T> : ISpecification<T>
{
    public ICollection<LambdaExpression> Expressions { get; }
    public int Type { get; }

    public int Skip { get; private set; }
    public int Take { get; private set; }

    protected BaseSpecification(int type)
    {
        Type = type;
        Expressions = new List<LambdaExpression>();
    }

    protected void AddObjExpression(Expression<Func<T, object>> expression)
    {
        Expressions.Add(expression);
    }

    protected void AddBoolExpression(Expression<Func<T, bool>> expression)
    {
        Expressions.Add(expression);
    }

    protected void AddPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }
}
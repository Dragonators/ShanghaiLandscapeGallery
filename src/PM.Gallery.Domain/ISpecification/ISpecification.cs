using System.Linq.Expressions;

namespace PM.Gallery.Domain.ISpecification;

public interface ISpecification<T>
{
    ICollection<LambdaExpression> Expressions { get; }
    int Type { get; }
    int Skip { get; }
    int Take { get; }
}
using PM.Gallery.Domain.ISpecification;

namespace PM.Gallery.Domain.IStrategies;

public interface IQueryStrategy<T, TDto> where T : class where TDto : class
{
    public void Apply(ISpecificationTools<T> finder, TDto dto);
}
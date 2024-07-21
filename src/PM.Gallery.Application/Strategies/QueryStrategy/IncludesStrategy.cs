using PM.Gallery.Application.Dtos;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Domain.IStrategies;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

namespace PM.Gallery.Application.Strategies.QueryStrategy;

public class IncludesStrategy : IQueryStrategy<Image, ImageQueryDto>
{
    public void Apply(ISpecificationTools<Image> finder, ImageQueryDto dto)
    {
        // if (dto.Tags != null && dto.Tags.Any())finder.Apply(new ImageIncludeTagsSpecification());
        // if (dto.SortOptions is  not null && dto.SortOptions.Any())finder.Apply(new ImageIncludeAllSpecification());
        finder.Apply(new ImageIncludeAllSpecification());
    }
}
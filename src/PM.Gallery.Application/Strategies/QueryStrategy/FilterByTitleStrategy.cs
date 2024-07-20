using PM.Gallery.Application.Dtos;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Domain.IStrategies;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

namespace PM.Gallery.Application.Strategies.QueryStrategy;

public class FilterByTitleStrategy : IQueryStrategy<Image, ImageQueryDto>
{
    public void Apply(ISpecificationTools<Image> finder, ImageQueryDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Title))
        {
            finder.Apply(new ImageWithTitleSpecification(dto.Title));
        }
    }
}
using PM.Gallery.Application.Dtos;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.IStrategies;

namespace PM.Gallery.Application.Strategies.QueryStrategy;

public static class StaticQueryStrategies
{
    public static List<IQueryStrategy<Image, ImageQueryDto>> Strategies { get; } =
        new()
        {
            new FilterByTagsStrategy(),
            new FilterByTitleStrategy(),
            new OrderStrategy(),
            new PaginationStrategy()
        };
}
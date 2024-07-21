using PM.Gallery.Application.Dtos;
using PM.Gallery.Application.Strategies.QueryStrategy.enums;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Domain.IStrategies;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

namespace PM.Gallery.Application.Strategies.QueryStrategy;

public class OrderStrategy : IQueryStrategy<Image, ImageQueryDto>
{
    public void Apply(ISpecificationTools<Image> finder, ImageQueryDto dto)
    {
        if (dto.SortOptions is null)
        {
            finder.Apply(new ImageOrderByUpdatedTimeSpecification(true, false));
            return;
        }

        var enumerator = dto.SortOptions.GetEnumerator();

        if (!enumerator.MoveNext())
        {
            finder.Apply(new ImageOrderByUpdatedTimeSpecification(true, false));
            return;
        }
        
        ApplyByKey(finder, enumerator.Current, false);
        while (enumerator.MoveNext())
        {
            ApplyByKey(finder, enumerator.Current, true);
        }
    }

    private static void ApplyByKey(ISpecificationTools<Image> finder, KeyValuePair<SortField, bool> sortOption, bool isThenBy)
    {
        switch (sortOption.Key)
        {
            case SortField.TagCount:
                finder
                    .Apply(new ImageOrderByTagCountSpecification(sortOption.Value, isThenBy));
                break;
            case SortField.UpdatedTime:
                finder
                    .Apply(new ImageOrderByUpdatedTimeSpecification(sortOption.Value, isThenBy));
                break;
            case SortField.CommentCount:
                finder
                    .Apply(new ImageOrderByCommentCountSpecification(sortOption.Value, isThenBy));
                break;
            case SortField.Rating:
                finder
                    .Apply(new ImageOrderByRatingSpecification(sortOption.Value, isThenBy));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
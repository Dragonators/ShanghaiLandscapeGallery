using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

public class ImageOrderByRatingSpecification : BaseSpecification<Image>
{
    public ImageOrderByRatingSpecification(bool ascending = true, bool isThenBy = false) : base(
        ascending
            ? (isThenBy ? (int)OperationType.ThenBy : (int)OperationType.OrderBy)
            : (isThenBy ? (int)OperationType.ThenByDescending : (int)OperationType.OrderByDescending))
    {
        AddObjExpression(i => i.Ratings.Average(r => r.Score));
    }
}
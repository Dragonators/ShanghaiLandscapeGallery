using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

public class ImageIncludeAllSpecification : BaseSpecification<Image>
{
    public ImageIncludeAllSpecification() : base((int)OperationType.Include)
    {
        AddObjExpression(i => i.Tags);
        AddObjExpression(i => i.Comments);
        AddObjExpression(i => i.Ratings);
    }
}
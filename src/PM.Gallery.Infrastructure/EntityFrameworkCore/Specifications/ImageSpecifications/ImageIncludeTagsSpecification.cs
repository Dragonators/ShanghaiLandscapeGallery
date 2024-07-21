using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

public class ImageIncludeTagsSpecification : BaseSpecification<Image>
{
    public ImageIncludeTagsSpecification() : base((int)OperationType.Include)
    {
        AddObjExpression(i => i.Tags);
    }
}
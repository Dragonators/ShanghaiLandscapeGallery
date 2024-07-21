using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

public class ImageWithTagsSpecification : BaseSpecification<Image>
{
    public ImageWithTagsSpecification(IEnumerable<string> tags)
        : base((int)OperationType.Where)
    {
        foreach (var tag in tags)
        {
            AddBoolExpression(i => i.Tags.Any(t => t.Name == tag));
        }

        // AddBoolExpression(i => tags.All(tag => i.Tags.Any(t => t.Name == tag)));
        // AddBoolExpression(i => i.Tags.Any(t => t.Name == tags.First()));
    }
}
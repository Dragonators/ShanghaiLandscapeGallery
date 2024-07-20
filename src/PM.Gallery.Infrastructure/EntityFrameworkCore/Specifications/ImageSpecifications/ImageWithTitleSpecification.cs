using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

public class ImageWithTitleSpecification : BaseSpecification<Image>
{
    public ImageWithTitleSpecification(string title)
        : base((int)OperationType.Where)
    {
        AddBoolExpression(i => i.Title == title);
    }
}
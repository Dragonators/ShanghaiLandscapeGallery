using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

public class ImageWithIdSpecification : BaseSpecification<Image>
{
    public ImageWithIdSpecification(Guid id)
        : base((int)OperationType.Where)
    {
        AddBoolExpression(i => i.Id == id);
    }
}
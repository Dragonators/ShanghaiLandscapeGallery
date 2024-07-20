using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Infrastructure.Specifications;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

public class ImagePagedSpecification : BaseSpecification<Image>
{
    public ImagePagedSpecification(int skip, int take) : base((int)OperationType.Paging)
    {
        AddPaging(skip, take);
    }
}
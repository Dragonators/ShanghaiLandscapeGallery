﻿using PM.Gallery.Application.Dtos;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ISpecification;
using PM.Gallery.Domain.IStrategies;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

namespace PM.Gallery.Application.Strategies.QueryStrategy;

public class FilterByTagsStrategy : IQueryStrategy<Image, ImageQueryDto>
{
    public void Apply(ISpecificationTools<Image> finder, ImageQueryDto dto)
    {
        if (dto.Tags != null && dto.Tags.Any())
        {
            finder.Apply(new ImageWithTagsSpecification(dto.Tags));
        }
    }
}
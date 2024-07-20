using Mapster;
using PM.Gallery.Application.Dtos;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ValueObjects;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

namespace PM.Gallery.Application.Services;

public partial class ImageService
{
    public async Task AddCommentAsync(Guid imageId, CommentDto commentDto)
    {
        var image = await ImageById(imageId);

        if (image != null)
        {
            var comment = commentDto.Adapt<Comment>();
            image.AddComment(comment);
            await _repository.UpdateAsync(image);
        }
        else
        {
            // _logger.LogError($"Image with id {imageId} not exists");
            throw new NullReferenceException();
        }
    }

    public async Task AddRatingAsync(Guid imageId, RatingDto ratingDto)
    {
        var image = await ImageById(imageId);

        if (image != null)
        {
            var rating = ratingDto.Adapt<Rating>();
            image.AddRating(rating);
            await _repository.UpdateAsync(image);
        }
        else
        {
            // _logger.LogError($"Image with id {imageId} not exists");
            throw new NullReferenceException();
        }
    }

    public async Task AddTagAsync(Guid imageId, TagDto tagDto)
    {
        var image = await ImageById(imageId);

        if (image != null)
        {
            var tag = tagDto.Adapt<Tag>();
            image.AddTag(tag);
            await _repository.UpdateAsync(image);
        }
        else
        {
            // _logger.LogError($"Image with id {imageId} not exists");
            throw new NullReferenceException();
        }
    }

    public async Task DeleteTagAsync(Guid imageId, TagDto tagDto)
    {
        var image = await ImageById(imageId);

        if (image != null)
        {
            var tag = tagDto.Adapt<Tag>();
            image.DeleteTag(tag);
            await _repository.UpdateAsync(image);
        }
        else
        {
            // _logger.LogError($"Image with id {imageId} not exists");
            throw new NullReferenceException();
        }
    }

    private async Task<Image?> ImageById(Guid id)
    {
        return await _repository.UseSpecification()
            .AsSplitQuery()
            .Apply(new ImageIncludeAllSpecification())
            .Apply(new ImageWithIdSpecification(id))
            .FirstOrDefaultAsync();
    }
}
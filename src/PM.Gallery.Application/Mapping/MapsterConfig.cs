using Mapster;
using PM.Gallery.Application.Dtos;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.ValueObjects;

namespace PM.Gallery.Application.Mapping;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Comment, Comment>.NewConfig()
            .MapWith(comment => comment);
        TypeAdapterConfig<Rating, Rating>.NewConfig()
            .MapWith(rating => rating);
        TypeAdapterConfig<Tag, Tag>.NewConfig()
            .MapWith(tag => tag);

        TypeAdapterConfig<CommentDto, Comment>.NewConfig()
            .ConstructUsing(src => new Comment(src.Text, src.CreatedAt));
        TypeAdapterConfig<RatingDto, Rating>.NewConfig()
            .ConstructUsing(src => new Rating(src.Score, src.CreatedAt));
        TypeAdapterConfig<TagDto, Tag>.NewConfig()
            .ConstructUsing(src => new Tag(src.Name, src.CreatedAt));

        TypeAdapterConfig<Image, ImageInfoDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ImageData, src => src.ImageData)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.LastUpdatedAt, src => src.LastUpdatedAt)
            .Map(dest => dest.Ratings, src => src.Ratings)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.Tags, src => src.Tags)
            .IgnoreNullValues(true);

        TypeAdapterConfig<ImageInfoDto, Image>.NewConfig()
            .ConstructUsing(src => new Image(src.Id, src.Title, src.ImageData, src.CreatedAt))
            .Map(dest => dest.Ratings, src => src.Ratings)
            .Map(dest => dest.Comments, src => src.Comments)
            .Map(dest => dest.Tags, src => src.Tags)
            .IgnoreNullValues(true);

        TypeAdapterConfig<Image, ImageDto>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.ImageData, src => src.ImageData)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.LastUpdatedAt, src => src.LastUpdatedAt);

        TypeAdapterConfig<ImageDto, Image>.NewConfig()
            .ConstructUsing(src => new Image(src.Id, src.Title, src.ImageData, src.CreatedAt));

        TypeAdapterConfig<ImageUpdateDto, Image>.NewConfig()
            .ConstructUsing(src => new Image(src.Id, src.Title, src.ImageData, src.LastUpdatedAt));
    }
}
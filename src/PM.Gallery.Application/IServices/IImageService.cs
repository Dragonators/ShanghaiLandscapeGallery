using PM.Gallery.Application.Dtos;

namespace PM.Gallery.Application.IServices;

public interface IImageService
{
    Task<ImageInfoDto?> GetImageByIdAsync(Guid id);
    IAsyncEnumerable<ImageDto> GetImagesStreamAsync();
    IAsyncEnumerable<ImageDto> FindImagesStreamAsync(ImageQueryDto imageQueryDto);
    Task<IEnumerable<ImageDto>> FindImagesAsync(ImageQueryDto imageQueryDto);
    Task AddImageAsync(ImageDto imageDto);
    Task UpdateImageAsync(ImageUpdateDto imageUpdateDto, Guid id);
    Task DeleteImageAsync(Guid id);
    Task AddImagesAsync(IEnumerable<ImageDto> imageDtos);
    Task DeleteImagesAsync(IEnumerable<Guid> ids);

    Task AddCommentAsync(Guid imageId, CommentDto commentDto);
    Task AddRatingAsync(Guid imageId, RatingDto ratingDto);
    Task AddTagAsync(Guid imageId, TagDto tagDto);
    Task DeleteTagAsync(Guid imageId, TagDto tagDto);
}
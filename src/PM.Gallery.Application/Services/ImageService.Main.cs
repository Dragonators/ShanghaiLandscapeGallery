using Mapster;
using PM.Gallery.Application.Dtos;
using PM.Gallery.Application.IServices;
using PM.Gallery.Application.Strategies.QueryStrategy;
using PM.Gallery.Domain.Entities;
using PM.Gallery.Domain.IRepository;
using PM.Gallery.Infrastructure.EntityFrameworkCore.Specifications.ImageSpecifications;

namespace PM.Gallery.Application.Services;

public partial class ImageService : IImageService
{
    private readonly IRepository<Image> _repository;
    private readonly ILogger<ImageService> _logger;

    public ImageService(IRepository<Image> repository, ILogger<ImageService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ImageInfoDto?> GetImageByIdAsync(Guid id)
    {
        var image = await ImageById(id);

        if (image is not null) return image.Adapt<ImageInfoDto>();
        
        _logger.LogWarning($"Image with id {id} not found");
        return null;

    }

    public async IAsyncEnumerable<ImageDto> GetImagesStreamAsync()
    {
        await foreach (var image in _repository.UseSpecification()
                           .AsNoTracking()
                           .Apply(new ImageOrderByUpdatedTimeSpecification())
                           .AsAsyncEnumerable())
        {
            yield return image.Adapt<ImageDto>();
        }
    }

    public async Task<IEnumerable<ImageDto>> GetImagesAsync()
    {
        var images = await _repository.UseSpecification()
            .AsNoTracking()
            .Apply(new ImageOrderByUpdatedTimeSpecification())
            .ToListAsync();
        return images.Adapt<IEnumerable<ImageDto>>();
    }

    public async IAsyncEnumerable<ImageDto> FindImagesStreamAsync(ImageQueryDto imageQueryDto)
    {
        var finder = _repository.UseSpecification()
            .Apply(new ImageIncludeAllSpecification())
            .AsNoTracking()
            .AsSplitQuery();

        foreach (var strategy in StaticQueryStrategies.Strategies)
        {
            strategy.Apply(finder, imageQueryDto);
        }

        await foreach (var image in finder.AsAsyncEnumerable())
        {
            yield return image.Adapt<ImageDto>();
        }
    }

    public async Task<IEnumerable<ImageDto>> FindImagesAsync(ImageQueryDto imageQueryDto)
    {
        var finder = _repository.UseSpecification()
            .Apply(new ImageIncludeAllSpecification())
            .AsNoTracking()
            .AsSplitQuery();

        foreach (var strategy in StaticQueryStrategies.Strategies)
        {
            strategy.Apply(finder, imageQueryDto);
        }

        var images = await finder.ToListAsync();
        return images.Adapt<IEnumerable<ImageDto>>();
    }

    public int CountImages()
    {
        return _repository.Count();
    }

    public async Task AddImageAsync(ImageDto imageDto)
    {
        var image = imageDto.Adapt<Image>();
        await _repository.AddAsync(image);
    }

    public async Task UpdateImageAsync(ImageUpdateDto imageUpdateDto, Guid id)
    {
        var image = await ImageById(id);
        if (image != null)
        {
            image.RenewImage(imageUpdateDto.Adapt<Image>());
            await _repository.UpdateAsync(image);
        }
        else
        {
            _logger.LogError($"Image with id {imageUpdateDto.Id} not exists");
            throw new NullReferenceException();
        }
    }

    public async Task DeleteImageAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task AddImagesAsync(IEnumerable<ImageDto> imageDtos)
    {
        await _repository.AddRangeAsync(imageDtos.Adapt<IEnumerable<Image>>());
    }

    public async Task DeleteImagesAsync(IEnumerable<Guid> ids)
    {
        await _repository.DeleteRangeAsync(ids);
    }
}
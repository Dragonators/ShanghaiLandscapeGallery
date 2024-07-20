using Microsoft.AspNetCore.Mvc;
using PM.Gallery.Application.Dtos;
using PM.Gallery.Application.IServices;

namespace PM.Gallery.HttpApi.Controllers;

[Route("api/v1/Image/{imageId:guid}/[controller]")]
[ApiController]
public class RatingController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly ILogger<RatingController> _logger;

    public RatingController(IImageService imageService, ILogger<RatingController> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddRatingAsync(Guid imageId, [FromBody] RatingDto ratingDto)
    {
        _logger.LogInformation("Starting AddRatingAsync with imageId: {imageId}", imageId);
        try
        {
            await _imageService.AddRatingAsync(imageId, ratingDto);
            _logger.LogInformation("Successfully added rating for ImageId: {ImageId}", imageId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding rating for imageId: {ImageId}", imageId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
        }
    }
}
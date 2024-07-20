using Microsoft.AspNetCore.Mvc;
using PM.Gallery.Application.Dtos;
using PM.Gallery.Application.IServices;

namespace PM.Gallery.HttpApi.Controllers;

[Route("api/v1/Image/{imageId:guid}/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly ILogger<TagController> _logger;

    public TagController(IImageService imageService, ILogger<TagController> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddTagAsync(Guid imageId, [FromBody] TagDto tagDto)
    {
        _logger.LogInformation("Starting AddTagAsync tag for ImageId: {ImageId}", imageId);
        try
        {
            await _imageService.AddTagAsync(imageId, tagDto);
            _logger.LogInformation("Successfully added tag for ImageId: {ImageId}", imageId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding tag for ImageId: {ImageId}", imageId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTagAsync(Guid imageId, [FromBody] TagDto tagDto)
    {
        _logger.LogInformation("Starting DeleteTagAsync tag for ImageId: {ImageId}", imageId);
        try
        {
            await _imageService.DeleteTagAsync(imageId, tagDto);
            _logger.LogInformation("Successfully deleted tag for ImageId: {ImageId}", imageId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting tag for ImageId: {ImageId}", imageId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
        }
    }
}
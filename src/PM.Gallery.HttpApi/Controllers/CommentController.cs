using Microsoft.AspNetCore.Mvc;
using PM.Gallery.Application.Dtos;
using PM.Gallery.Application.IServices;

namespace PM.Gallery.HttpApi.Controllers;

[Route("api/v1/Image/{imageId:guid}/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly ILogger<CommentController> _logger;

    public CommentController(IImageService imageService, ILogger<CommentController> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddCommentAsync(Guid imageId, [FromBody] CommentDto commentDto)
    {
        _logger.LogInformation("Starting AddCommentAsync for ImageId: {ImageId}", imageId);
        try
        {
            await _imageService.AddCommentAsync(imageId, commentDto);
            _logger.LogInformation("Successfully added comment for ImageId: {ImageId}", imageId);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding comment for ImageId: {ImageId}", imageId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
        }
    }
}
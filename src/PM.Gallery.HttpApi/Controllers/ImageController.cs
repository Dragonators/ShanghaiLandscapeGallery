using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using PM.Gallery.Application.Dtos;
using PM.Gallery.Application.IServices;

namespace PM.Gallery.HttpApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IImageService imageService, ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }

        [HttpGet]
        public async Task GetImagesStreamAsync()
        {
            _logger.LogInformation("Starting GetImagesStreamAsync");

            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            try
            {
                await foreach (var image in _imageService.GetImagesStreamAsync())
                {
                    await Response.WriteAsync($"data: {JsonSerializer.Serialize(image)}\n\n");
                    await Response.Body.FlushAsync();
                }

                _logger.LogInformation("Successfully streamed images");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error streaming images");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                await Response.WriteAsync($"data: {{\"error\": \"{ex.Message}\"}}\n\n");
            }
        }

        [HttpGet("{imageId:guid}")]
        public async Task<IActionResult> GetImageByIdAsync(Guid imageId)
        {
            try
            {
                var image = await _imageService.GetImageByIdAsync(imageId);

                if (image == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Successfully retrieved image for ImageId: {imageId}",imageId);
                return Ok(image);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        // [HttpGet("search")]
        // public async Task<IActionResult> SearchImagesStreamAsync([FromQuery] ImageQueryDto imageQueryDto)
        // {
        //     var image = await _imageService.FindImagesStreamAsync(imageQueryDto);
        //     return Ok(image);
        // }

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> DeleteImageAsync(Guid imageId)
        {
            await _imageService.DeleteImageAsync(imageId);
            return Ok();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddImagesAsync([FromBody] IEnumerable<ImageDto> imageDtos)
        {
            await _imageService.AddImagesAsync(imageDtos);
            return Ok();
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateImageAsync([FromBody] ImageUpdateDto imageUpdateDto)
        {
            await _imageService.UpdateImageAsync(imageUpdateDto);
            return Ok();
        }
    }
}
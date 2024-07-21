using System.Reflection;
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

        [HttpGet("search")]
        public async Task SearchImagesStreamAsync([FromQuery] ImageQueryDto imageQueryDto)
        {
            _logger.LogInformation("Starting FindImagesStreamAsync");

            Response.Headers.Add("Content-Type", "text/event-stream");
            Response.Headers.Add("Cache-Control", "no-cache");
            try
            {
                await foreach (var image in _imageService.FindImagesStreamAsync(imageQueryDto))
                {
                    await Response.WriteAsync($"data: {JsonSerializer.Serialize(image)}\n\n");
                    await Response.Body.FlushAsync();
                }

                _logger.LogInformation("Successfully find images");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error find images as stream");
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                await Response.WriteAsync($"data: {{\"error\": \"{ex.Message}\"}}\n\n");
            }
        }

        [HttpGet("count")]
        public IActionResult GetImagesCountAsync()
        {
            return Ok(_imageService.CountImages());
        }
        

        [HttpDelete("{imageId:guid}")]
        public async Task<IActionResult> DeleteImageAsync(Guid imageId)
        {
            _logger.LogInformation("Starting DeleteImageAsync");
            
            try
            {
                await _imageService.DeleteImageAsync(imageId);
                _logger.LogInformation("Successfully delete image with id {imageId}", imageId);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error delete image with id {imageId}", imageId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPut("{imageId:guid}")]
        public async Task<IActionResult> UpdateImageAsync([FromBody] ImageUpdateDto imageUpdateDto, Guid imageId)
        {
            _logger.LogInformation("Starting UpdateImageAsync");

            try
            {
                await _imageService.UpdateImageAsync(imageUpdateDto, imageId);
                _logger.LogInformation("Successfully update image with id {imageId}", imageId);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error update image with id {imageId}", imageId);
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
        
        [HttpPost("batch")]
        public async Task<IActionResult> AddImagesAsync([FromBody] IEnumerable<ImageDto> imageDtos)
        {
            _logger.LogInformation("Starting AddImagesAsync");

            try
            {
                await _imageService.AddImagesAsync(imageDtos);
                _logger.LogInformation("Successfully add images");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error add images");
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
        
        [HttpDelete("batch")]
        public async Task<IActionResult> DeleteImagesAsync([FromBody] IEnumerable<Guid> ids)
        {
            _logger.LogInformation("Starting DeleteImagesAsync");

            try
            {
                await _imageService.DeleteImagesAsync(ids);
                _logger.LogInformation("Successfully delete images");
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error delete images");
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
    }
}
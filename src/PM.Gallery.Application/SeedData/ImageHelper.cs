using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PM.Gallery.Application.SeedData;

public static class ImageHelper
{
    public static async Task<byte[]> ImageToBytes(string imagePath)
    {
        if (!File.Exists(imagePath))
        {
            throw new FileNotFoundException("The specified image file does not exist.", imagePath);
        }

        using var image = await Image.LoadAsync<Rgba32>(imagePath);
        using var memoryStream = new MemoryStream();
        await image.SaveAsync(memoryStream, image.Metadata.DecodedImageFormat!);
        return memoryStream.ToArray();
    }
}
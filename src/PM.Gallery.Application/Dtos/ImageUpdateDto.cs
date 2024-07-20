namespace PM.Gallery.Application.Dtos;

public class ImageUpdateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public byte[] ImageData { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}
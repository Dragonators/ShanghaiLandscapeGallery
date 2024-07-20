using PM.Gallery.Domain.ValueObjects;

namespace PM.Gallery.Application.Dtos;

public class ImageInfoDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public byte[] ImageData { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Tag>? Tags { get; set; }
}
using System.ComponentModel.DataAnnotations;
using PM.Gallery.Domain.ValueObjects;

namespace PM.Gallery.Domain.Entities;

public class Image : BaseModel<Guid>
{
    [Required] [MaxLength(30)] public string Title { get; private set; }
    [Required] public byte[] ImageData { get; private set; }
    public virtual ICollection<Rating> Ratings { get; }
    public virtual ICollection<Comment> Comments { get; }
    public virtual ICollection<Tag> Tags { get; }

    public Image(Guid id, string title, byte[] imageData, DateTime createdAt) : base(createdAt, id)
    {
        Title = title;
        ImageData = imageData;
        Ratings = new List<Rating>();
        Comments = new List<Comment>();
        Tags = new List<Tag>();
    }

    public void RenewImage(Image image)
    {
        Title = image.Title;
        ImageData = image.ImageData;
        LastUpdatedAt = image.LastUpdatedAt;
    }

    public void AddRating(Rating rating)
    {
        Ratings.Add(rating);
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public void AddTag(Tag tag)
    {
        if (Tags.Any(t => t.Name == tag.Name))
        {
            throw new InvalidOperationException($"A tag with the name '{tag.Name}' already exists in this collection.");
        }

        Tags.Add(tag);
    }

    public void DeleteTag(Tag tag)
    {
        var existingTag = Tags.FirstOrDefault(t => t.Name == tag.Name);
        if (existingTag == null)
        {
            throw new InvalidOperationException($"A tag with the name '{tag.Name}' does not exist in this collection.");
        }

        Tags.Remove(existingTag);
    }
}
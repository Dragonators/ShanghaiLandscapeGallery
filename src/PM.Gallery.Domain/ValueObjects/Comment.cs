using System.ComponentModel.DataAnnotations;

namespace PM.Gallery.Domain.ValueObjects;

public class Comment : ValueObject
{
    [MaxLength(1000)] [Required] public string Text { get; private set; }

    public Comment(string text, DateTime createdAt) : base(createdAt)
    {
        Text = text;
    }
}
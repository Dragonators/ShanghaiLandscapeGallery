using System.ComponentModel.DataAnnotations;

namespace PM.Gallery.Domain.ValueObjects;

public class Tag : ValueObject
{
    [MaxLength(30)] [Required] public string Name { get; private set; }

    public Tag(string name, DateTime createdAt) : base(createdAt)
    {
        Name = name;
    }
}
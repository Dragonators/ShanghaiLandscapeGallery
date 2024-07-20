using System.ComponentModel.DataAnnotations;

namespace PM.Gallery.Domain.ValueObjects;

public class Rating : ValueObject
{
    [Required] public int Score { get; private set; }

    public Rating(int score, DateTime createdAt) : base(createdAt)
    {
        Score = score;
    }
}
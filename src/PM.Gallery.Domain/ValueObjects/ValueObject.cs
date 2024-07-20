namespace PM.Gallery.Domain.ValueObjects;

public abstract class ValueObject
{
    public DateTime CreatedAt { get; protected set; }

    protected ValueObject(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }
}
namespace PM.Gallery.Domain.Entities;

public abstract class BaseModel<T>
{
    public T Id { get; }

    public DateTime CreatedAt { get; }
    public DateTime LastUpdatedAt { get; protected set; }

    protected BaseModel(DateTime createdAt,T id)
    {
        Id = id;
        CreatedAt = createdAt;
        LastUpdatedAt = createdAt;
    }
}
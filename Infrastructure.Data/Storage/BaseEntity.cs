namespace Infrastructure.Data.Storage;

public abstract class BaseEntity<T>
{
    public T ID { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

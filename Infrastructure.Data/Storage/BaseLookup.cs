namespace Infrastructure.Data.Storage;

public abstract class BaseLookup : BaseEntity<int>
{
    public string Name { get; set; }
    public string NameAr { get; set; }
    public bool IsActive { get; set; }

    public static implicit operator int(BaseLookup lookup) => lookup.ID;
}

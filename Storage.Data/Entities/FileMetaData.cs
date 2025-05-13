using Infrastructure.Data.Storage;
using Storage.Data.Entities.Lookups;

namespace Storage.Data.Entities;

public class FileMetaData : BaseEntity<Guid>
{
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Path { get; set; }
    public int StorageProviderTypeID { get; set; }
    public virtual StorageProviderType StorageProviderType { get; set; }
}

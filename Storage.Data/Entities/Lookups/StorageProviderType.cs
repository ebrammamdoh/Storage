using Infrastructure.Data.Storage;

namespace Storage.Data.Entities.Lookups;

public class StorageProviderType : BaseLookup
{

    public virtual ICollection<FileMetaData> FileMetaData { get; set; }

    #region Data

    public static StorageProviderType Azure_Blob = new StorageProviderType
    {
        ID = 1,
        CreatedAt = DateTime.Now,
        Name = "Azure Blob",
        NameAr = "Azure Blob",
    };

    public static StorageProviderType AWS_S3 = new StorageProviderType
    {
        ID = 2,
        CreatedAt = DateTime.Now,
        Name = "AWS_S3",
        NameAr = "AWS_S3",
    };

    public static StorageProviderType OnPrem = new StorageProviderType
    {
        ID = 3,
        CreatedAt = DateTime.Now,
        Name = "On Prem",
        NameAr = "On Prem",
    };

    #endregion

    public static List<StorageProviderType> Data = [
        Azure_Blob, AWS_S3, OnPrem
        ];
}

using Storage.Data.Entities.Lookups;
using Storage.Services.StorageService;

namespace Storage.Services.FactoryHelper;

public interface IStorageFactoryHelper
{
    IFileStorageService GetService(int storageTypeId);
}

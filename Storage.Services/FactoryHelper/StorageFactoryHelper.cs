using Microsoft.Extensions.DependencyInjection;
using Storage.Data.Entities.Lookups;
using Storage.Services.StorageService;

namespace Storage.Services.FactoryHelper;

public class StorageFactoryHelper : IStorageFactoryHelper
{
    private readonly IServiceProvider _serviceProvider;
    public StorageFactoryHelper(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IFileStorageService GetService(int storageTypeId)
    {
        return storageTypeId switch
        {
            var x when x == StorageProviderType.Azure_Blob => ActivatorUtilities.CreateInstance<AzureBlobStorageService>(_serviceProvider),

            var x when x == StorageProviderType.AWS_S3 => ActivatorUtilities.CreateInstance<AWSS3StorageService>(_serviceProvider),

            _ => throw new NotSupportedException()
        };
    }
}

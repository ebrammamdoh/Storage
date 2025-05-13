using Infrastructure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Storage.Data.Entities;
using Storage.Services.FactoryHelper;
using Storage.Services.Repositories;

namespace Storage.Services.StorageHelper;

public class FileStorageHelper : IFileStorageHelper
{
    private readonly IStorageFactoryHelper _storageFactoryHelper;
    private readonly IConfiguration _configuration;
    private readonly IBaseRepository<FileMetaData, Guid> _metadataRepo;

    public FileStorageHelper(IStorageFactoryHelper storageFactoryHelper, IConfiguration configuration)
    {
        _storageFactoryHelper = storageFactoryHelper;
        _configuration = configuration;
    }

    public async Task DeleteAsync(Guid fileId)
    {
        var metadata = await _metadataRepo.GetByIdAsync(fileId);
        if(metadata is null)
        {
            throw new GeneralException("-3", "Invalid data");
        }

        await _storageFactoryHelper.GetService(metadata.StorageProviderTypeID).DeleteAsync(fileId, metadata.Path);
    }

    public async Task<Stream> DownloadAsync(Guid fileId)
    {
        var metadata = await _metadataRepo.GetByIdAsync(fileId);
        if (metadata is null)
        {
            throw new GeneralException("-3", "Invalid data");
        }

        return await _storageFactoryHelper.GetService(metadata.StorageProviderTypeID).DownloadAsync(fileId, metadata.Path);
    }

    public async Task<Guid> UploadAsync(IFormFile file)
    {
        var typeId = _configuration.GetValue<int>("CurrentStorageTypeId");
        return await _storageFactoryHelper.GetService(typeId).UploadAsync(file);
    }
}

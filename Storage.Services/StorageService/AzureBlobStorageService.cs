using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Storage.Data.Entities;
using Storage.Data.Entities.Lookups;
using Storage.Services.Repositories;
using System.Data.Entity.Core.Common;

namespace Storage.Services.StorageService;

public class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobContainerClient _containerClient;
    private readonly IBaseRepository<FileMetaData, Guid> _metadataRepo;
    private readonly IUnitOfWork _unitOfWork;

    public AzureBlobStorageService(BlobContainerClient containerClient, IBaseRepository<FileMetaData, Guid> metadataRepo, IUnitOfWork unitOfWork)
    {
        _containerClient = containerClient;
        _metadataRepo = metadataRepo;
        _unitOfWork = unitOfWork;
    }


    public async Task<Guid> UploadAsync(IFormFile file)
    {
        var id = Guid.NewGuid();
        var blobName = $"files/{id}/{file.FileName}";
        var blobClient = _containerClient.GetBlobClient(blobName);

        await using var stream = file.OpenReadStream();
        await blobClient.UploadAsync(stream);

        var metadata = new FileMetaData
        {
            ID = id,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Size = file.Length,
            Path = blobName,
            StorageProviderTypeID = StorageProviderType.Azure_Blob
        };
        _metadataRepo.Add(metadata);
        await _unitOfWork.SaveChangesAsync();
        return id;
    }

    public async Task<Stream> DownloadAsync(Guid fileId, string path)
    {
        var blobClient = _containerClient.GetBlobClient(path);
        var download = await blobClient.DownloadAsync();
        return download.Value.Content;
    }

    public async Task DeleteAsync(Guid fileId, string path)
    {
        var blobClient = _containerClient.GetBlobClient(path);
        await blobClient.DeleteIfExistsAsync();
        await _metadataRepo.DeleteAsync(x => x.ID == fileId);
    }
}

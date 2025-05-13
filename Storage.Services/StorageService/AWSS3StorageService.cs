using Microsoft.AspNetCore.Http;
using Storage.Data.Entities;
using Storage.Data.Entities.Lookups;
using Storage.Services.Repositories;

namespace Storage.Services.StorageService;

public class AWSS3StorageService : IFileStorageService
{
    private readonly IBaseRepository<FileMetaData, Guid> _metadataRepo;
    private readonly IUnitOfWork _unitOfWork;

    public AWSS3StorageService(IBaseRepository<FileMetaData, Guid> metadataRepo, IUnitOfWork unitOfWork)
    {
        _metadataRepo = metadataRepo;
        _unitOfWork = unitOfWork;
    }

    public Task DeleteAsync(Guid fileId, string path)
    {
        //ToDO: S3 implementation
        throw new NotImplementedException();
    }

    public Task<Stream> DownloadAsync(Guid fileId, string path)
    {
        //ToDO: S3 implementation
        throw new NotImplementedException();
    }

    public async Task<Guid> UploadAsync(IFormFile file)
    {
        var id = Guid.NewGuid();
        //ToDO: S3 implementation

        var metadata = new FileMetaData
        {
            ID = id,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Size = file.Length,
            //Path = blobName,
            StorageProviderTypeID = StorageProviderType.AWS_S3
        };
        _metadataRepo.Add(metadata);
        await _unitOfWork.SaveChangesAsync();
        return id;
    }
}

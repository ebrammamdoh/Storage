using Microsoft.AspNetCore.Http;
using Storage.Services.StorageService;

namespace Storage.Services.StorageHelper;

public interface IFileStorageHelper
{
    //IFileStorageService GetService();
    Task<Guid> UploadAsync(IFormFile file);
    Task<Stream> DownloadAsync(Guid fileId);
    Task DeleteAsync(Guid fileId);
}

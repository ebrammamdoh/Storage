using Microsoft.AspNetCore.Http;

namespace Storage.Services.StorageService;

public interface IFileStorageService
{
    Task<Guid> UploadAsync(IFormFile file);
    Task<Stream> DownloadAsync(Guid fileId, string path);
    Task DeleteAsync(Guid fileId, string path);
}

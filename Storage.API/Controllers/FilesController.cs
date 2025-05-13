using Infrastructure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Storage.API.Controllers.Base;
using Storage.API.Models.Config;
using Storage.Services.StorageHelper;

namespace Storage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : BaseController
    {
        private readonly IFileStorageHelper _storageHelper;
        private readonly Dictionary<string, long> _allowedExtensions;
        public FilesController(IFileStorageHelper storageHelper, IOptions<FileValidationOptions> options)
        {
            _storageHelper = storageHelper;
            _allowedExtensions = options.Value.AllowedExtensions
                    .ToDictionary(e => e.Key.ToLowerInvariant(), e => e.Value);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_allowedExtensions.TryGetValue(extension, out var maxSize))
                return BadRequest("-1", "Unsupported file type.");

            if (file.Length > maxSize)
                return BadRequest("-2", $"File size exceeds the limit for {extension}. Max allowed: {maxSize / (1024 * 1024)} MB.");
            try
            {
                var id = await _storageHelper.UploadAsync(file);
                return Ok(new { FileId = id });
            }
            catch (GeneralException ex)
            {
                return BadRequest(ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download(Guid id)
        {
            try
            {
                var stream = await _storageHelper.DownloadAsync(id);
                return File(stream, "application/octet-stream");
            }
            catch (GeneralException ex)
            {
                return BadRequest(ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _storageHelper.DeleteAsync(id);
                return NoContent();
            }
            catch (GeneralException ex)
            {
                return BadRequest(ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

namespace Storage.API.Models.Config;

public class FileValidationOptions
{
    public Dictionary<string, long> AllowedExtensions { get; set; } = new();
}

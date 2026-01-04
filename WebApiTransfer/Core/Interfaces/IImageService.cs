using Microsoft.AspNetCore.Http;

namespace Core.Interfaces;

public interface IImageService
{
    public Task<string> UploadImageAsync(IFormFile file);
    Task<string> SaveImageAsync(byte[] bytes);
    void DeleteImage(string fileName);
    Task<string> SaveImageFromUrlAsync(string imageUrl);
}

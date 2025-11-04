namespace WorkingMVC.Interfaces;

public interface IImageService
{
    public Task<string> UploadImageAsync(IFormFile file); 
}
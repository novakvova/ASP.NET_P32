namespace WorkingMVC.Interfaces;

public interface IImageService
{
    public Task<string> UploadImage(IFormFile file); 
}
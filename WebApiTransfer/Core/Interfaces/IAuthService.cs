namespace Core.Interfaces;

public interface IAuthService
{
    Task<int> GetUserIdAsync();
}

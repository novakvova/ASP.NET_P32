namespace Core.Interfaces;

public interface IAccountService
{
    public Task<string> LoginByGoogle(string token);
}
using Microsoft.AspNetCore.Http;

namespace Core.Models.Account;

public class RegisterModel
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public IFormFile Image { get; set; } = null!;
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Account;
using Core.Models.Location.City;
using Core.SMTP;
using Domain;
using Domain.Entities.Idenity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Services;

public class UserService(IAuthService authService, 
    AppDbTransferContext transferContext,
    IConfiguration configuration,
    UserManager<UserEntity> userManager,
    ISmtpService smtpService,
    IMapper mapper) : IUserService
{
    public async Task<bool> ForgotPasswordAsync(ForgotPasswordModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return false;
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"{configuration["ClientUrl"]}/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(model.Email)}";

        var emailModel = new EmailMessage
        {
            To = model.Email,
            Subject = "Password Reset",
            Body = $"<p>Click the link below to reset your password:</p><a href='{resetLink}'>Reset Password</a>"
        };

        var result = await smtpService.SendEmailAsync(emailModel);

        return result;
    }

    public async Task<UserProfileModel> GetUserProfileAsync()
    {
        var userId = await authService.GetUserIdAsync();
        
        var profile = await transferContext.Users
            .ProjectTo<UserProfileModel>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(u => u.Id == userId!);

        return profile!;
    }

    public async Task ResetPasswordAsync(ResetPasswordModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);

        if (user != null)
            await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
    }
}

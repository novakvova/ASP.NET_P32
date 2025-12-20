using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Account;
using Core.Models.Location.City;
using Core.Models.Search;
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

    public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded)
            {
                return false;
            }
        }
        else
            return false;

        return true;
    }

    public async Task<SearchResult<UserItemModel>> SearchAsync(UserSearchModel model)
    {
        var query = transferContext.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(model.Name))
        {
            string nameFilter = model.Name.Trim().ToLower();
            query = query.Where(u =>
                (u.FirstName + " " + u.LastName).ToLower().Contains(nameFilter)
                || u.FirstName.ToLower().Contains(nameFilter)
                || u.LastName.ToLower().Contains(nameFilter));
        }

        if (model?.StartDate != null)
        {
            query = query.Where(u => u.DateCreated >= model.StartDate);
        }
        if (model?.StartDate != null)
        {
            query = query.Where(u => u.DateCreated <= model.EndDate);
        }
        //кількіть загальних елементів для пагінації
        var totalItems = await query.CountAsync();
        //кількість записів на сторінку
        var safeItemsPerPage = model.ItemPerPage < 1 ? 10 : model.ItemPerPage;
        //Кількість записву ділене на кількість сторінок і округлення в більшу сторону
        var totalPages = (int)Math.Ceiling((double)totalItems / safeItemsPerPage);
        //Безпечна поточна сторінка
        var safePage = Math.Min(Math.Max(1, model.Page), Math.Max(1, totalPages));
        var users = await query
            .OrderBy(u => u.Id)
            .Skip((safePage - 1) * safeItemsPerPage) //пропускаємо елементи для попередніх сторінок
            .Take(safeItemsPerPage) //беремо елементи для поточної сторінки
            .ProjectTo<UserItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
        var result = new SearchResult<UserItemModel>
        {
            Items = users,
            Pagination = new PaginationModel
            {
                TotalCount = totalItems,
                TotalPages = totalPages,
                ItemsPerPage = safeItemsPerPage,
                CurrentPage = safePage
            }
        };
        return result;
    }
}

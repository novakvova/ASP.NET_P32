using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Cart;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class CartService(AppDbTransferContext appDbContext,
    IAuthService authService,
    IMapper mapper) : ICartService
{
    public async Task AddUpdateAsync(CartAddUpdateModel model)
    {
        var userId = await authService.GetUserIdAsync();
        var cartItem = await appDbContext.Carts
            .SingleOrDefaultAsync(c => c.UserId == userId && 
                c.TransportationId == model.TransportationId);
        if (cartItem == null)
        {
            cartItem = new Domain.Entities.CartEntity
            {
                UserId = userId,
                TransportationId = model.TransportationId,
                CountTikets = model.Quantity
            };
            await appDbContext.Carts.AddAsync(cartItem);
        }
        else
        {
            cartItem.CountTikets = model.Quantity;
        }
        await appDbContext.SaveChangesAsync();
    }

    public async Task<List<CartItemModel>> GetListAsync()
    {
        var userId = await authService.GetUserIdAsync();
        var result = await appDbContext.Carts
            .Where(c => c.UserId == userId)
            .ProjectTo<CartItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return result;
    }
}

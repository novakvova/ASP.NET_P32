using Core.Models.Cart;

namespace Core.Interfaces;

public interface ICartService
{
    Task AddUpdateAsync(CartAddUpdateModel model);
    Task<List<CartItemModel>> GetListAsync();
}

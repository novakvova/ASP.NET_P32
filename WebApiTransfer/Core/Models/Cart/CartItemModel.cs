using Core.Models.Transportation;

namespace Core.Models.Cart;

public class CartItemModel : TransportationItemModel
{
    public short Quantity { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingMVC.Data.Entities;

[Table("tblProducts")]
public class ProductEntity : BaseEntity<int>
{
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public CategoryEntity Category { get; set; } = null!;
    public ICollection<ProductImageEntity>? ProductImages { get; set; }
    public ICollection<CartEntity> Carts { get; set; } = null!;
    public ICollection<OrderItemEntity> OrderItems { get; set; } = null!;
}

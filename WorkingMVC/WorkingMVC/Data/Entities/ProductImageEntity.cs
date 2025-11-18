using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingMVC.Data.Entities;

[Table("tblProductImages")]
public class ProductImageEntity : BaseEntity<int>
{
    [StringLength(250)]
    public string Name { get; set; } = String.Empty;
    public short Priority { get; set; }
    [ForeignKey(nameof(Product))]
    public int ProductId { get; set; }
    public ProductEntity? Product { get; set; }
}

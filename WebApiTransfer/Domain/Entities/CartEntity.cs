using Domain.Entities.Idenity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("tblCarts")]
public class CartEntity
{
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    [ForeignKey(nameof(Transportation))]
    public int TransportationId { get; set; }

    public short CountTikets{ get; set; }
    public virtual UserEntity? User { get; set; }
    public virtual TransportationEntity? Transportation { get; set; }
}

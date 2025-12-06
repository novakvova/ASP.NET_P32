using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("tblTransportationStatuses")]
public class TransportationStatusEntity : BaseEntity<int>
{
    [StringLength(255)]
    public string Name { get; set; } = String.Empty;
    public ICollection<TransportationEntity>? Transportations { get; set; }
}

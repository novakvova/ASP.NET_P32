using Core.Models.Transportation;

namespace Core.Interfaces;

public interface ITransportationService
{
    Task<List<TransportationItemModel>> GetListAsync();
}

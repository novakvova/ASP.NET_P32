using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Transportation;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class TransportationService(
    AppDbTransferContext appDbContext,
    IMapper mapper) : ITransportationService
{
    public async Task<List<TransportationItemModel>> GetListAsync()
    {
        var result = await appDbContext.Transportations
            .ProjectTo<TransportationItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return result;
    }
}

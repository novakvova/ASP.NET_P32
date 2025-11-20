using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class AppDbTransferContext : DbContext
{
    public AppDbTransferContext(DbContextOptions<AppDbTransferContext> options)
        : base(options)
    {
    }

    public DbSet<CountryEntity> Countries { get; set; }
}

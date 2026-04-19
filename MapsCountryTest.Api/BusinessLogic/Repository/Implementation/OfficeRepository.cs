using MapsCountryTest.Api.Repository.Abstraction;
using MapsCountryTest.Api.BusinessLogic.MapCountryTest.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

namespace MapsCountryTest.Api.Repository.Implementation
{
    public class OfficeRepository : BaseRepository<MapsCountryTestDbContext, Office>, IOfficeRepository
    {
        public OfficeRepository(MapsCountryTestDbContext dbContext) : base(dbContext)
        {
        }
        /// <inheritdoc/>>
        public async Task<int> GetCityIdAsync(string city, string region, CancellationToken cancellationToken)
        {
            
            var result = await _dbSet.Where(x=>x.AddressCity ==  city && x.AddressRegion == region)
                .Select(x=>x.CityCode).FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        /// <inheritdoc/>>
        public async Task<IEnumerable<Office>> GetOfficesAsync(string city, string region, CancellationToken cancellationToken)
        {
            var result = await _dbSet.
                Where(x => x.AddressCity == city && x.AddressRegion == region)
                .Include(x=>x.Phones)
                .Include(x=>x.Coordinates)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}

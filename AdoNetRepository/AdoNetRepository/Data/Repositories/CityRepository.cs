using AdoNetRepository.Data.Entities;
using Microsoft.Data.SqlClient;

namespace AdoNetRepository.Data.Repositories;

public class CityRepository : BaseRepository<City>
{
    public CityRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override Task<IList<City>> ReadDataAsync(SqlDataReader reader)
    {
        throw new NotImplementedException();
    }
}
using AdoNetRepository.Data.Entities;
using Microsoft.Data.SqlClient;

namespace AdoNetRepository.Data.Repositories;

public class CityRepository : BaseRepository<City>
{
    public CityRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override IList<City> ReadDataAsync(SqlDataReader reader)
    {
        var cities = new List<City>();

        while (reader.Read())
        {
            cities.Add(new City
            {
                CountryId = new Guid(reader["CountryId"].ToString() ?? string.Empty),
                Id = new Guid(reader["Id"].ToString() ?? string.Empty),
                Name = reader["Name"].ToString() ?? string.Empty,
            });
        }

        return cities;
    }

    protected override string GetQueryForUpdate(City entity, string queryRaw)
    {
        throw new NotImplementedException();
    }

    protected override string GetQueryForInsert(City entity, string queryRaw)
    {
        throw new NotImplementedException();
    }
}
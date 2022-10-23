using AdoNetRepository.Data.Entities;
using AdoNetRepository.Data.Models;
using Microsoft.Data.SqlClient;

namespace AdoNetRepository.Data.Repositories;

public class CountryRepository : BaseRepository<Country>
{
    
    public CountryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override IList<Country> ReadDataAsync(SqlDataReader reader)
    {
        var countries = new List<CountryResponse>();

        while (reader.Read())
        {
            countries.Add(new CountryResponse
            {
                CountryId = new Guid(reader["CountryId"].ToString() ?? string.Empty),
                CityId = new Guid(reader["CityId"].ToString() ?? string.Empty),
                AirportId = new Guid(reader["AirportId"].ToString() ?? string.Empty),
                CountryName = reader["CountryName"].ToString() ?? string.Empty,
                CityName = reader["CityName"].ToString() ?? string.Empty,
                AirportName = reader["AirportName"].ToString() ?? string.Empty,
            });
        }

        return GetCountries(countries);
    }

    protected override string GetQueryForUpdate(Country entity, string queryRaw)
    {
        return string.Format(queryRaw, entity.Name, entity.Id);
    }

    protected override string GetQueryForInsert(Country entity, string queryRaw)
    {
        return string.Format(queryRaw, entity.Id.ToString(), entity.Name);
    }

    private IList<Country> GetCountries(IList<CountryResponse> countryReads)
    {
        var countries = countryReads.GroupBy(x => x.CountryId).Select(GetCountry).ToList();

        return countries;
    }

    private Country GetCountry(IGrouping<Guid, CountryResponse> countryReads)
    {
        var cityGroups = countryReads.GroupBy(x => x.CityId);

        var country = new Country
        {
            Id = countryReads.First().CountryId,
            Name = countryReads.First().CountryName,
            Cities = new List<City>(),
        };

        foreach (var cityGroup in cityGroups)
        {
            country.Cities.Add(new City
            {
                Id = cityGroup.First().CityId,
                Name = cityGroup.First().CityName,
                CountryId = country.Id,
                Airports = cityGroup.Select(x => new Airport{Id = x.AirportId, Name = x.AirportName, CityId = x.CityId}).ToList(),
            });
        }

        return country;
    }
}
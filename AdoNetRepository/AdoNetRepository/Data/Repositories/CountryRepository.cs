using AdoNetRepository.Data.Entities;
using AdoNetRepository.Data.Models;
using Microsoft.Data.SqlClient;
using MoreLinq;

namespace AdoNetRepository.Data.Repositories;

public class CountryRepository : BaseRepository<Country>
{
    private readonly CityRepository _cityRepository;
    
    public CountryRepository(IConfiguration configuration, CityRepository cityRepository) : base(configuration)
    {
        _cityRepository = cityRepository;
    }

    protected override async Task<IList<Country>> ReadDataAsync(SqlDataReader reader)
    {
        var countries = new List<CountryRead>();

        while (reader.Read())
        {
            countries.Add(new CountryRead
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

    private IList<Country> GetCountries(IList<CountryRead> countryReads)
    {
        var countries = countryReads.GroupBy(x => x.CountryId).Select(GetCountry).ToList();

        return countries;
    }

    private Country GetCountry(IGrouping<Guid, CountryRead> countryReads)
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
using AdoNetRepository.Data.Entities;
using AdoNetRepository.Data.Models;
using Microsoft.Data.SqlClient;

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
        var countries = new List<Country>();

        foreach (var countryRead in countryReads)
        {
            var country = new Country
            {
                Id = countryRead.CountryId,
                Name = countryRead.CountryName,
                Cities = countryReads.Where(x => x.CityId == countryRead.CityId)
                    .Select(y => 
                        new City
                        {
                            Id = y.CityId,
                            Name = y.CityName,
                        }).ToList(),
            };
        }

        return countries;
    }
}
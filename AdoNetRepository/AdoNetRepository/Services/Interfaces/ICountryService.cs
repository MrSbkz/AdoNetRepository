using AdoNetRepository.Data.Entities;

namespace AdoNetRepository.Services.Interfaces;

public interface ICountryService
{
    Task<IList<Country>> GetCountriesAsync();

    Task<Country?> GetCountryByIdAsync(Guid id);

    Task<Country> UpdateCountryAsync(Country country);

    Task AddCountryAsync(string countryName);

    Task DeleteCountry(Guid countryId);
}
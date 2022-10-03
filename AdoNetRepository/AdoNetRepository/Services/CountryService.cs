using AdoNetRepository.Data.Entities;
using AdoNetRepository.Data.Repositories;
using AdoNetRepository.Services.Interfaces;

namespace AdoNetRepository.Services;

public class CountryService : ICountryService
{
    private readonly CountryRepository _countryRepository;

    public CountryService(CountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<IList<Country>> GetCountriesAsync()
    {
        return await _countryRepository.GetListAsync();
    }

    public async Task<Country?> GetCountryByIdAsync(Guid id)
    {
        return await _countryRepository.GetByIdAsync(id);
    }

    public async Task<Country> UpdateCountryAsync(Country country)
    {
        await _countryRepository.UpdateAsync(country);
        return (await _countryRepository.GetByIdAsync(country.Id))!;
    }

    public async Task AddCountryAsync(string countryName)
    {
        await _countryRepository.AddAsync(new Country {Id = Guid.NewGuid(), Name = countryName});
    }

    public async Task DeleteCountry(Guid countryId)
    {
        await _countryRepository.DeleteAsync(countryId);
    }
}
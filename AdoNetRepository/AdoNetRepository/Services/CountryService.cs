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
}
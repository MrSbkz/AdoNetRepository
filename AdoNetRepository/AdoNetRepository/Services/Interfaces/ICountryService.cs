using AdoNetRepository.Data.Entities;

namespace AdoNetRepository.Services.Interfaces;

public interface ICountryService
{
    Task<IList<Country>> GetCountriesAsync();
}
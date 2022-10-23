using AdoNetRepository.Data;
using AdoNetRepository.Data.Entities;
using AdoNetRepository.Data.Repositories;
using AdoNetRepository.Services.Interfaces;

namespace AdoNetRepository.Services;

public class CountryService : ICountryService
{
    private readonly UnitOfWork _unitOfWork;

    public CountryService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<Country>> GetCountriesAsync()
    {
        return await _unitOfWork.Countries.GetListAsync();
    }

    public async Task<Country?> GetCountryByIdAsync(Guid id)
    {
        return await _unitOfWork.Countries.GetSingleAsync(id);
    }

    public async Task<Country> UpdateCountryAsync(Country country)
    {
        await _unitOfWork.Countries.UpdateAsync(country);
        return (await _unitOfWork.Countries.GetSingleAsync(country.Id))!;
    }

    public async Task AddCountryAsync(string countryName)
    {
        await _unitOfWork.Countries.AddAsync(new Country {Id = Guid.NewGuid(), Name = countryName});
    }

    public async Task DeleteCountry(Guid countryId)
    {
        await _unitOfWork.Countries.DeleteAsync(countryId);
    }
}
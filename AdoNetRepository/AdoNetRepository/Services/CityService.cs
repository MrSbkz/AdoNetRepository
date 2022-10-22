using AdoNetRepository.Data.Entities;
using AdoNetRepository.Data.Repositories;
using AdoNetRepository.Services.Interfaces;

namespace AdoNetRepository.Services;

public class CityService : ICityService
{
    private readonly CityRepository _cityRepository;

    public CityService(CityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IList<City>> GetCitiesAsync()
    {
        return await _cityRepository.GetListAsync();
    }
}
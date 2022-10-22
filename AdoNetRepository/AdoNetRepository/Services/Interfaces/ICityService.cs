using AdoNetRepository.Data.Entities;

namespace AdoNetRepository.Services.Interfaces;

public interface ICityService
{
    Task<IList<City>> GetCitiesAsync();
}
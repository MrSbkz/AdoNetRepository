using AdoNetRepository.Data;
using AdoNetRepository.Data.Entities;
using AdoNetRepository.Services.Interfaces;

namespace AdoNetRepository.Services;

public class CityService : ICityService
{
    private readonly UnitOfWork _unitOfWork;

    public CityService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<City>> GetCitiesAsync()
    {
        return await _unitOfWork.Cities.GetListAsync();
    }
}
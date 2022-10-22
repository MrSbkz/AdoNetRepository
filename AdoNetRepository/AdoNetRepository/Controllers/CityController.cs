using AdoNetRepository.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdoNetRepository.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> GetCountriesAsync()
    {
        return Ok(await _cityService.GetCitiesAsync());
    }
}
using AdoNetRepository.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AdoNetRepository.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCountriesAsync()
    {
        return Ok(await _countryService.GetCountriesAsync());
    }
}
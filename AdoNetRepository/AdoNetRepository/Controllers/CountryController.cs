using AdoNetRepository.Data.Entities;
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
    
    [HttpGet("list")]
    public async Task<IActionResult> GetCountriesAsync()
    {
        return Ok(await _countryService.GetCountriesAsync());
    }
    
    [HttpGet]
    [Route("{countryId:guid}")]
    public async Task<IActionResult> GetCountryAsync(Guid countryId)
    {
        return Ok(await _countryService.GetCountryByIdAsync(countryId));
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCountryAsync([FromBody] Country country)
    {
        return Ok(await _countryService.UpdateCountryAsync(country));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCountryAsync(string countryName)
    {
        await _countryService.AddCountryAsync(countryName);
        return Ok();
    }
    
    [HttpDelete]
    [Route("{countryId:guid}")]
    public async Task<IActionResult> DeleteCountryAsync(Guid countryId)
    {
        await _countryService.DeleteCountry(countryId);
        return Ok();
    }
}
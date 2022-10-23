namespace AdoNetRepository.Data.Models;

public class CountryResponse
{
    public Guid CountryId { get; set; }

    public Guid CityId { get; set; }

    public Guid AirportId { get; set; }

    public string CountryName { get; set; } = string.Empty;

    public string CityName { get; set; } = string.Empty;

    public string AirportName { get; set; } = string.Empty;
}
namespace AdoNetRepository.Data.Entities;

public class Airport
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid CityId { get; set; }
}
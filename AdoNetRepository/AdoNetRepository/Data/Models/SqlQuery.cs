namespace AdoNetRepository.Data.Models;

public class SqlQuery
{
    public string GetList { get; set; } = string.Empty;

    public string GetById { get; set; } = string.Empty;

    public string GetRelatedList { get; set; } = string.Empty;

    public string Update { get; set; } = string.Empty;

    public string Delete { get; set; } = string.Empty;

    public string Add { get; set; } = string.Empty;
}
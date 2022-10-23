namespace AdoNetRepository.Data.Entities;

public class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public List<string>? Roles { get; set; }
}
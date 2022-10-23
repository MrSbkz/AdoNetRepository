namespace AdoNetRepository.Data.Models;

public class UserResponse
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;
}
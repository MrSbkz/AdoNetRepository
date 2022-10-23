namespace AdoNetRepository.Services.Interfaces;

public interface IAuthService
{
    Task RegisterUserAsync(string userName, string password);

    Task<string> LoginAsync(string userName, string password);
}
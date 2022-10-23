using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AdoNetRepository.Data;
using AdoNetRepository.Data.Entities;
using AdoNetRepository.Services.Interfaces;
using Azure.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AdoNetRepository.Services;

public class AuthService : IAuthService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(UnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task RegisterUserAsync(string userName, string password)
    {
        var existingUser = await _unitOfWork.Users.GetSingleAsync(userName);

        if (existingUser != null)
        {
            throw new AuthenticationException("User already exists");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            PasswordHash = GetHash(password),
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.UserRoles.AddAsync(new UserRoles
        {
            UserId = user.Id,
            RoleId = (await _unitOfWork.Roles.GetSingleAsync("user"))!.Id,
        });
    }

    public async Task<string> LoginAsync(string userName, string password)
    {
        var user = await _unitOfWork.Users.GetSingleAsync(userName);
        if (user == null || !user.PasswordHash.Equals(GetHash(password)))
        {
            throw new AuthenticationFailedException("Incorrect user name or password");
        }

        return GetJwtToken(user);
    }

    private string GetJwtToken(User user)
    {
        var authClaims = new List<Claim>
        {
            new (ClaimTypes.Name, user.UserName),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        foreach (var userRole in user.Roles!)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(30),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GetHash(string text)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}
using AdoNetRepository.Data.Entities;
using AdoNetRepository.Data.Models;
using Microsoft.Data.SqlClient;

namespace AdoNetRepository.Data.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override IList<User> ReadDataAsync(SqlDataReader reader)
    {
        var userResponses = new List<UserResponse>();

        while (reader.Read())
        {
            userResponses.Add(new UserResponse
            {
                UserId = new Guid(reader["UserId"].ToString() ?? string.Empty),
                UserName = reader["UserName"].ToString() ?? string.Empty,
                PasswordHash = reader["PasswordHash"].ToString() ?? string.Empty,
                RoleName = reader["RoleName"].ToString() ?? string.Empty,
            });
        }

        return GetUsers(userResponses);
    }

    protected override string GetQueryForUpdate(User entity, string queryRaw)
    {
        return string.Empty;
    }

    protected override string GetQueryForInsert(User entity, string queryRaw)
    {
        return string.Format(queryRaw, entity.Id, entity.UserName, entity.PasswordHash);
    }

    private IList<User> GetUsers(IList<UserResponse> userResponses)
    {
        var users = new List<User>();

        foreach (var userResponse in userResponses)
        {
            if (users.Any(x => x.Id == userResponse.UserId))
            {
                continue;
            }
            
            users.Add(new User
            {
                Id = userResponse.UserId,
                UserName = userResponse.UserName,
                PasswordHash = userResponse.PasswordHash,
                Roles = userResponses.Where(x => x.UserId == userResponse.UserId).Select(x => x.RoleName).ToList(),
            });
        }

        return users;
    }
}
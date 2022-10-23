using AdoNetRepository.Data.Entities;
using Microsoft.Data.SqlClient;

namespace AdoNetRepository.Data.Repositories;

public class RoleRepository : BaseRepository<Role>
{
    public RoleRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override IList<Role> ReadDataAsync(SqlDataReader reader)
    {
        var roles = new List<Role>();

        while (reader.Read())
        {
            roles.Add(new Role
            {
                Id = new Guid(reader["Id"].ToString() ?? string.Empty),
                Name = reader["Name"].ToString() ?? string.Empty,
            });
        }

        return roles;
    }

    protected override string GetQueryForUpdate(Role entity, string queryRaw)
    {
        return string.Empty;
    }

    protected override string GetQueryForInsert(Role entity, string queryRaw)
    {
        return string.Format(queryRaw, entity.Id, entity.Name);
    }
}
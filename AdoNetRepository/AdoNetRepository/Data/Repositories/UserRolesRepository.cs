using AdoNetRepository.Data.Entities;
using Microsoft.Data.SqlClient;

namespace AdoNetRepository.Data.Repositories;

public class UserRolesRepository : BaseRepository<UserRoles>
{
    public UserRolesRepository(IConfiguration configuration) : base(configuration)
    {
    }

    protected override IList<UserRoles> ReadDataAsync(SqlDataReader reader)
    {
        return new List<UserRoles>();
    }

    protected override string GetQueryForUpdate(UserRoles entity, string queryRaw)
    {
        return string.Empty;
    }

    protected override string GetQueryForInsert(UserRoles entity, string queryRaw)
    {
        return string.Format(queryRaw, entity.UserId, entity.RoleId);
    }
}
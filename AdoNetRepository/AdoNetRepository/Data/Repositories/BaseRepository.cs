using System.Data;
using AdoNetRepository.Data.Models;
using Microsoft.Data.SqlClient;

namespace AdoNetRepository.Data.Repositories;

public abstract class BaseRepository<TEntity>
{
    private readonly string _connectionString;
    private readonly SqlQuery _sqlQuery;

    protected BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        var entityName = typeof(TEntity).Name;
        _sqlQuery = configuration.GetSection(entityName).Get<SqlQuery>();
    }

    public async Task<IList<TEntity>> GetListAsync(string? condition = null)
    {
        var query = $"{_sqlQuery.GetList} {condition}";
        await using var sqlConnection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(query, sqlConnection);
        cmd.CommandType = CommandType.Text;
        sqlConnection.Open();

        var reader = await cmd.ExecuteReaderAsync();

        var entities = await ReadDataAsync(reader);

        return entities;
    }

    private async Task<SqlDataReader> ExecuteAsync(string query)
    {
        await using var sqlConnection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(query, sqlConnection);
        cmd.CommandType = CommandType.Text;
        sqlConnection.Open();

        return await cmd.ExecuteReaderAsync();
    }

    protected abstract Task<IList<TEntity>> ReadDataAsync(SqlDataReader reader);
}
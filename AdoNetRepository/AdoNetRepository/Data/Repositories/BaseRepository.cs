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

    public async Task<IList<TEntity>> GetListAsync()
    {
        await using var sqlConnection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(_sqlQuery.GetList, sqlConnection);
        cmd.CommandType = CommandType.Text;
        sqlConnection.Open();
        
        var reader = await cmd.ExecuteReaderAsync();

        var entities = ReadDataAsync(reader);

        return entities;
    }

    public async Task<TEntity?> GetByIdAsync(object id)
    {
        var query = string.Format(_sqlQuery.GetById, id);
        
        await using var sqlConnection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(query, sqlConnection);
        cmd.CommandType = CommandType.Text;
        sqlConnection.Open();
        
        var reader = await cmd.ExecuteReaderAsync();

        var entities = ReadDataAsync(reader);

        return entities.FirstOrDefault();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var query = GetQueryForUpdate(entity, _sqlQuery.Update);
        
        await using var sqlConnection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(query, sqlConnection);
        cmd.CommandType = CommandType.Text;
        sqlConnection.Open();
        
        await cmd.ExecuteReaderAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        var query = GetQueryForInsert(entity, _sqlQuery.Add);
        
        await using var sqlConnection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(query, sqlConnection);
        cmd.CommandType = CommandType.Text;
        sqlConnection.Open();
        
        await cmd.ExecuteReaderAsync();
    }

    public async Task DeleteAsync(object id)
    {
        var query = string.Format(_sqlQuery.Delete, id);
        
        await using var sqlConnection = new SqlConnection(_connectionString);
        var cmd = new SqlCommand(query, sqlConnection);
        cmd.CommandType = CommandType.Text;
        sqlConnection.Open();
        
        await cmd.ExecuteReaderAsync();
    }

    protected abstract IList<TEntity> ReadDataAsync(SqlDataReader reader);

    protected abstract string GetQueryForUpdate(TEntity entity, string queryRaw);

    protected abstract string GetQueryForInsert(TEntity entity, string queryRaw);
}
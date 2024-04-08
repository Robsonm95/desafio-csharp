using Dapper;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Data;

namespace CleanArch.Infrastructure.Repositories;

public class MovimentoRepository : IMovimentoRepository
{
    private readonly IDbConnection _dbConnection;

    public MovimentoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Movimento>> Get()
    {
        string query = "SELECT * FROM Members";
        return await _dbConnection.QueryAsync<Movimento>(query);
    }

    public async Task<Movimento> GetById(int id)
    {
        string query = "SELECT * FROM Members WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Movimento>(query, new { Id = id });
    }

    public Task<IEnumerable<Movimento>> GetByNumero(int id)
    {
        string query = "SELECT * FROM Members WHERE Id = @Id";
        return await _dbConnection.QueryAsync<Movimento>(query, new { Id = id });
    }

    public Task<Movimento> AddMember(Movimento member)
    {
        throw new NotImplementedException();
    }
}

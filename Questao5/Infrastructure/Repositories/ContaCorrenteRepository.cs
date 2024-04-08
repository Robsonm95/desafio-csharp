using CleanArch.Domain.Entities;
using Dapper;
using Questao5.Domain.Abstraction;
using Questao5.Domain.Entities;
using System.Data;

namespace CleanArch.Infrastructure.Repositories;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly IDbConnection _dbConnection;

    public ContaCorrenteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ContaCorrente> GetById(int id)
    {
        string query = "SELECT * FROM Members WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { Id = id });
    }

    public async Task<IEnumerable<ContaCorrente>> Get()
    {
        string query = "SELECT * FROM Members";
        return await _dbConnection.QueryAsync<ContaCorrente>(query);
    }
}

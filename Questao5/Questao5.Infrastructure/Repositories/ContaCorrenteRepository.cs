using Dapper;
using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repositories;

public class ContaCorrenteRepository : IContaCorrenteRepository
{
    private readonly IDbConnection _dbConnection;

    public ContaCorrenteRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<ContaCorrente> GetContaCorrenteById(string id)
    {
        string query = "SELECT * FROM contacorrente WHERE idcontacorrente = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { Id = id });
    }

    
}

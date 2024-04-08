using Dapper;
using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using System.Data;

namespace Questao5.Infrastructure.Repositories;

public class IdempotenciaRepository : IIdempotenciaRepository
{
    private readonly IDbConnection _dbConnection;

    public IdempotenciaRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Idempotencia> GetIdempotenciaById(string id)
    {
        string query = "SELECT * FROM idempotencia  WHERE chave_idempotencia = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Idempotencia>(query, new { Id = id });
    }
    
    public async Task<Idempotencia> AddIdempotencia(Idempotencia idempotencia)
    {
        string query = "INSERT INTO idempotencia(chave_idempotencia, requisicao ,resultado) VALUES (@Chave_Idempotencia, @Requisicao, @Resultado)";
        await _dbConnection.ExecuteScalarAsync<int>(query, idempotencia);
        return idempotencia;
    }
}

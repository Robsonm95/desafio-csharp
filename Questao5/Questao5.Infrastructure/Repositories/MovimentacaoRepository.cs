using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using Dapper;
using System.Data;
using System;

namespace Questao5.Infrastructure.Repositories;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly IDbConnection _dbConnection;

    public MovimentacaoRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Movimento>> GetMovimentacaoById(string id)
    {
        string query = "SELECT * FROM movimento WHERE idcontacorrente = @Id";
        return await _dbConnection.QueryAsync<Movimento>(query, new { Id = id });
    }

    public async Task<Movimento> AddMovimentacao(Movimento movimento)
    {
        movimento.IdMovimento = Guid.NewGuid().ToString();

        string query = "INSERT INTO movimento(idmovimento, idcontacorrente,datamovimento,tipomovimento, valor) VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento,@Valor);";
        await _dbConnection.ExecuteScalarAsync<int>(query, movimento);
        return movimento;
    }
}

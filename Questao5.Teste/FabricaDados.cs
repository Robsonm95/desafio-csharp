using Questao5.Application.Movimento.Commands;
using Questao5.Domain.Entities;
using System.Text.Json;

namespace Questao5.Teste;

public class FabricaDados
{
    public static Movimento GetMovimento(string idContaCorrente, string tipo = "C", decimal valor = 600)
    {
        return new Movimento()
        {
            IdMovimento = Guid.NewGuid().ToString(),
            IdContaCorrente = idContaCorrente,
            DataMovimento = "24/06/2021",
            TipoMovimento = tipo,
            Valor = valor
        };
    }

    public static CreateMovimentoCommand GetRequest(string tipo = "C")
    {
        return new CreateMovimentoCommand()
        {
            IdRequest = Guid.NewGuid().ToString(),
            DataMovimento = "24/06/2021",
            IdContaCorrente = Guid.NewGuid().ToString(),
            TipoMovimento = tipo,
            Valor = 600
        };
    }

    public static Idempotencia GetIdempotencia()
    {
        return new Idempotencia()
        {
            Chave_Idempotencia = Guid.NewGuid().ToString(),
            Requisicao = JsonSerializer.Serialize(GetRequest()),
            Resultado = JsonSerializer.Serialize(GetMovimento(Guid.NewGuid().ToString()))
        };
    }

    public static ContaCorrente GetContaCorrente(bool ativo = true)
    {
        return new ContaCorrente()
        {
            Ativo = ativo,
            IdContaCorrente = Guid.NewGuid().ToString(),
            Nome = "Teste",
            Numero = 500
        };
    }
}

using FluentValidation;
using NSubstitute;
using Questao5.Application.Members.Commands;
using Questao5.Application.Members.Queries;
using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using static Questao5.Application.Members.Commands.CreateMovimentoCommand;
using static Questao5.Application.Members.Queries.SaldoContaByIdQuery;

namespace Questao5.Teste.Queries;

public class SaldoContaByIdQueryTest
{
    private readonly CancellationToken _cancellationToken;
    public SaldoContaByIdQueryTest()
    {
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public void ConsultaSaldoComSucesso()
    {
        // Arrange
        var movimentoRepository = Substitute.For<IMovimentacaoRepository>();
        SaldoContaByIdQuery request = new SaldoContaByIdQuery()
        {
            Id = Guid.NewGuid().ToString(),
        };

        var id = Guid.NewGuid().ToString();
         var listaMovimento = new List<Movimento>();
        listaMovimento.Add(FabricaDados.GetMovimento(id, "C", 500)); //  +  500
        listaMovimento.Add(FabricaDados.GetMovimento(id, "C", 900)); //  +  900
        listaMovimento.Add(FabricaDados.GetMovimento(id, "C", 100)); //  +  100

        listaMovimento.Add(FabricaDados.GetMovimento(id, "D", 100)); // - 100
        listaMovimento.Add(FabricaDados.GetMovimento(id, "D", 100)); // - 100
        listaMovimento.Add(FabricaDados.GetMovimento(id, "D", 100)); // - 100
        listaMovimento.Add(FabricaDados.GetMovimento(id, "D", 200)); // - 200
        // resultado esperado 1000

        movimentoRepository.GetMovimentacaoById(Arg.Any<string>()).Returns(listaMovimento);

        var commandHandler = new GetMovimentacaoByIdQueryHandler(movimentoRepository);

        // Act
        var resultado = commandHandler.Handle(request, _cancellationToken);

        // Assert
        Assert.Equal(1000, resultado.Result.Saldo);

    }

}

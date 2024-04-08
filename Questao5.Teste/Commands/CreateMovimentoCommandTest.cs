using FluentValidation;
using NSubstitute;
using Questao5.Application.Members.Commands;
using Questao5.Domain.Abstractions;
using Questao5.Domain.Entities;
using System.Text.Json;
using static Questao5.Application.Members.Commands.CreateMovimentoCommand;

namespace Questao5.Teste.Commands;

public class CreateMovimentoCommandTest
{
    private readonly CancellationToken _cancellationToken;
    public CreateMovimentoCommandTest()
    {
        _cancellationToken = new CancellationToken();
    }

    [Fact]
    public void CriaMovimentacaoComSucesso()
    {
        // Arrange
        var movimentoRepository = Substitute.For<IMovimentacaoRepository>();
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
        var validator = Substitute.For<IValidator<CreateMovimentoCommand>>();
        

        CreateMovimentoCommand request = FabricaDados.GetRequest();
        Movimento retorno = FabricaDados.GetMovimento(Guid.NewGuid().ToString());

        contaCorrenteRepository.GetContaCorrenteById(request.IdContaCorrente).Returns(FabricaDados.GetContaCorrente(ativo: true));
        movimentoRepository.AddMovimentacao(Arg.Any<Movimento>()).Returns(retorno);

        var commandHandler = new CreateMovimentoCommandHandler(movimentoRepository, contaCorrenteRepository, idempotenciaRepository, validator);

        // Act
        var resultado = commandHandler.Handle(request, _cancellationToken);
    
        // Assert
        Assert.Equal(retorno, resultado.Result);

    }

    [Fact]
    public void NaoCriaMovimentacao_IdempotenciaRetornaRegistro()
    {
        // Arrange
        var movimentoRepository = Substitute.For<IMovimentacaoRepository>();
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
        var validator = Substitute.For<IValidator<CreateMovimentoCommand>>();
        

        CreateMovimentoCommand request = FabricaDados.GetRequest();

        var idempotencia = FabricaDados.GetIdempotencia();
        idempotenciaRepository.GetIdempotenciaById(request.IdRequest).Returns(idempotencia);


        var commandHandler = new CreateMovimentoCommandHandler(movimentoRepository, contaCorrenteRepository, idempotenciaRepository, validator);

        // Act
        var resultado = commandHandler.Handle(request, _cancellationToken);
        var result = JsonSerializer.Deserialize<Movimento>(idempotencia.Resultado);
        // Assert
        Assert.Equal(result.TipoMovimento, resultado.Result.TipoMovimento);
        Assert.Equal(result.IdMovimento, resultado.Result.IdMovimento);
        Assert.Equal(result.DataMovimento, resultado.Result.DataMovimento);
        Assert.Equal(result.Valor, resultado.Result.Valor);


    }

    [Fact]
    public void NaoCriaMovimentacao_NaoEncontradoContaCorrente()
    {
        // Arrange
        var movimentoRepository = Substitute.For<IMovimentacaoRepository>();
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
        var validator = Substitute.For<IValidator<CreateMovimentoCommand>>();
        
        CreateMovimentoCommand request = FabricaDados.GetRequest();
        Movimento retorno = FabricaDados.GetMovimento(Guid.NewGuid().ToString());;

        var commandHandler = new CreateMovimentoCommandHandler(movimentoRepository, contaCorrenteRepository, idempotenciaRepository, validator);

        // Act
        var resultado = commandHandler.Handle(request, _cancellationToken);

        // Assert
        Assert.Equal("One or more errors occurred. (INVALID_ACCOUNT)", resultado.Exception.Message);
    }

    [Fact]
    public void NaoCriaMovimentacao_EncontradoContaCorrenteInativa()
    {
        // Arrange
        var movimentoRepository = Substitute.For<IMovimentacaoRepository>();
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
        var validator = Substitute.For<IValidator<CreateMovimentoCommand>>();
        
        CreateMovimentoCommand request = FabricaDados.GetRequest();
        Movimento retorno = FabricaDados.GetMovimento(Guid.NewGuid().ToString());


        contaCorrenteRepository.GetContaCorrenteById(request.IdContaCorrente).Returns(FabricaDados.GetContaCorrente(ativo: false));
      
        var commandHandler = new CreateMovimentoCommandHandler(movimentoRepository, contaCorrenteRepository, idempotenciaRepository, validator);

        // Act
        var resultado = commandHandler.Handle(request, _cancellationToken);

        //Assert
        Assert.Equal("One or more errors occurred. (INACTIVE_ACCOUNT)", resultado.Exception.Message);
      
    }

    [Fact]
    public void NaoCriaMovimentacao_TipoContaCorrenteInativa()
    {
        // Arrange
        var movimentoRepository = Substitute.For<IMovimentacaoRepository>();
        var contaCorrenteRepository = Substitute.For<IContaCorrenteRepository>();
        var idempotenciaRepository = Substitute.For<IIdempotenciaRepository>();
        var validator = Substitute.For<IValidator<CreateMovimentoCommand>>();
        

        CreateMovimentoCommand request = FabricaDados.GetRequest("F");
        Movimento retorno = FabricaDados.GetMovimento(Guid.NewGuid().ToString());

        contaCorrenteRepository.GetContaCorrenteById(request.IdContaCorrente).Returns(FabricaDados.GetContaCorrente(ativo: true));

        var commandHandler = new CreateMovimentoCommandHandler(movimentoRepository, contaCorrenteRepository, idempotenciaRepository, validator);

        // Act
        var resultado = commandHandler.Handle(request, _cancellationToken);

        //Assert
        Assert.Equal("One or more errors occurred. (INVALID_TYPE)", resultado.Exception.Message);

    }
}

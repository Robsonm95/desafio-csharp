using MediatR;
using NSubstitute;
using Questao5.API.Controllers;
using Questao5.Application.Movimento.Commands;
using Questao5.Domain.Entities;

namespace Questao5.Teste.NewFolder
{
    public class MovimentacaoContaControllerTest
    {
        [Fact]
        public void CriarMovimentacaoComSucesso()
        {

            // Arrange
            var _mediator = Substitute.For<IMediator>();
            CreateMovimentoCommand request = FabricaDados.GetRequest();
            Movimento retorno = FabricaDados.GetMovimento(Guid.NewGuid().ToString());

            _mediator.Send(request).Returns(retorno);
            var controller = new MovimentacaoContaController(_mediator);

            // Act
            var actual = controller.CreateMovimento(request);

            //var atu = actual.Result.mi;
            // Assert
            Assert.Equal(null, actual.Exception);
            //Assert.Equal(retorno, actual.Result.Returns.);
            // Assert.Equal(0, actual);

        }

    }
}
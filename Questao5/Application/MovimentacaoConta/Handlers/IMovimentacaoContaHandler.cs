using Questao5.Application.MovimentacaoConta.Commands.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Application.MovimentacaoConta.Handlers
{
    public interface IMovimentacaoContaHandler
    {
        MovimentacaoContaResponse Handle(MovimentacaoContaRequest request);
    }
}

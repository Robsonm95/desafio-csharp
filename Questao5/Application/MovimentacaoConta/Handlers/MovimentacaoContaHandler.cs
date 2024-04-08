using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Database.CommandStore.Responses;

namespace Questao5.Application.MovimentacaoConta.Handlers
{
    public class MovimentacaoContaHandler : IMovimentacaoContaHandler
    {
        public MovimentacaoContaResponse Handle(MovimentacaoContaRequest request)
        {
            return new MovimentacaoContaResponse();
        }
    }
}

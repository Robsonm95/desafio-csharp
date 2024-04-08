using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstractions;

public interface IMovimentacaoRepository
{
    Task<IEnumerable<Movimento>> GetMovimentacaoById(string id);
    Task<Movimento> AddMovimentacao(Movimento movimento);
}

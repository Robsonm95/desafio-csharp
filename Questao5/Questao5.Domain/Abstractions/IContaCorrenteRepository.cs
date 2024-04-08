using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstractions;

public interface IContaCorrenteRepository
{
    Task<ContaCorrente> GetContaCorrenteById(string id);
}

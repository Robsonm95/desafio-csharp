using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstraction;

public interface IContaCorrenteRepository
{
    //Task<IEnumerable<ContaCorrente>> Get();
    Task<ContaCorrente> GetById(int id);
}
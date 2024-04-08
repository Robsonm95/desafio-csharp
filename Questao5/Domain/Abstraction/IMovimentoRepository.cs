using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstraction;

public interface IMovimentoRepository
{
    Task<Movimento> GetById(int id);
    Task<IEnumerable<Movimento>> GetByNumero(int id);
    Task<Movimento> AddMember(Movimento member);
 
}
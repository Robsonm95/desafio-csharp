

using Questao5.Domain.Abstraction;

namespace CleanArch.Domain.Entities;

public interface IUnitOfWork
{
    IContaCorrenteRepository ContaCorrenteRepository { get; }
    Task CommitAsync();
}

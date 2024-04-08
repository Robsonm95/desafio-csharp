using Questao5.Domain.Entities;

namespace Questao5.Domain.Abstractions;

public interface IIdempotenciaRepository
{
    Task<Idempotencia> GetIdempotenciaById(string id);
    Task<Idempotencia> AddIdempotencia(Idempotencia movimento);
}

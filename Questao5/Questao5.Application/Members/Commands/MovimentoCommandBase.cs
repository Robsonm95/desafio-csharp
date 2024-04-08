using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Movimento.Commands;

public abstract class MovimentoCommandBase : IRequest<Domain.Entities.Movimento>
{
    public string IdRequest { get; set; } = String.Empty;
    public string IdContaCorrente { get; set; } = String.Empty;
    public string DataMovimento { get; set; } = String.Empty;
    public string TipoMovimento { get; set; } = String.Empty;
    public decimal Valor { get; set; }

}

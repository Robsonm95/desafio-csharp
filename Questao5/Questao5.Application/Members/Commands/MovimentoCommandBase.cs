using MediatR;
using Questao5.Domain.Entities;

namespace Questao5.Application.Members.Commands;

public abstract class MovimentoCommandBase : IRequest<Movimento>
{
    public string IdRequest { get; set; } = String.Empty;
    public string IdContaCorrente { get; set; } = String.Empty;
    public string DataMovimento { get; set; } = String.Empty;
    public string TipoMovimento { get; set; } = String.Empty;
    public decimal Valor { get; set; }

}

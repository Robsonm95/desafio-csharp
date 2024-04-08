using Questao5.Domain.Enumerators;

namespace Questao5.Application.MovimentacaoConta.Commands.Requests;

public class MovimentacaoContaRequest
{
    public Guid IdMovimento { get; set; }
    public int IdContaCorrente { get; set; }
    public TipoMovimento TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}

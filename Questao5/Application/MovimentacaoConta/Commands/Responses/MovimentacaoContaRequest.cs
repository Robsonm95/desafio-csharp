using Questao5.Domain.Enumerators;

namespace Questao5.Application.MovimentacaoConta.Commands.Responses;

public class MovimentacaoContaResponse
{
    public Guid IdMovimento { get; set; }
    public int IdContaCorrente { get; set; }
    public TipoMovimento TipoMovimento { get; set; }
    public decimal Valor { get; set; }
}

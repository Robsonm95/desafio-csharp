namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    public Guid IdContaCorrente { get; set; }
    public int Numero { get; set; }
    public string Nome { get; set; } = String.Empty;
    public bool Ativo { get; set; }
}

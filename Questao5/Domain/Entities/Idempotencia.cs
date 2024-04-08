namespace Questao5.Domain.Entities;

public class Idempotencia
{
    public Guid ChaveIdempotencia { get; set; }
    public string Requisicao { get; set; } = String.Empty;
    public string Resultado { get; set; } = String.Empty;

}

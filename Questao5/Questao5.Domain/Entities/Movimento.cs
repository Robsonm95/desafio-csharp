using Questao5.Domain.Enumerators;
using Questao5.Domain.Validation;
using System.Text.Json.Serialization;

namespace Questao5.Domain.Entities;

public sealed class Movimento
{
    public string IdMovimento { get; set; }
    public string IdContaCorrente { get; set; }
    public string DataMovimento { get; set; } = String.Empty;
    public string TipoMovimento { get; set; } = String.Empty;
    public decimal Valor { get; set; }

    public Movimento(string idContaCorrente, string dataMovimento, string tipoMovimento, decimal valor)
    {
        ValidateDomain(idContaCorrente, dataMovimento, tipoMovimento, valor);
    }
    public Movimento() { }

    [JsonConstructor]
    public Movimento(string idMovimento, string idContaCorrente, string dataMovimento, string tipoMovimento, decimal valor)
    {

        IdMovimento = idMovimento;
        ValidateDomain( idContaCorrente, dataMovimento, tipoMovimento, valor);
    }

    private void ValidateDomain(string idContaCorrente, string dataMovimento, string tipoMovimento, decimal valor)
    {
        DomainValidation.When(string.IsNullOrEmpty(idContaCorrente.ToString()),
            "Invalida Conta Corrente. Conta Corrente is required");

        DomainValidation.When(dataMovimento.Length < 3,
            "Invalid Data Movimento, muito curso, Minimo 3 characteres");
        
        DomainValidation.When(valor <= 0,
            "INVALID_VALUE");

        IdContaCorrente = idContaCorrente;
        DataMovimento = dataMovimento;
        TipoMovimento = tipoMovimento;


        Valor = valor;
    }
}

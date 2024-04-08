using System.Globalization;

namespace Questao1;

class ContaBancaria
{
    private int numero;
    private string titular;
    private double saldo;
    private double taxaSaque = 3.5;

    public ContaBancaria(int numero, string titular, double depositoInicial = 0)
    {
        this.numero = numero;
        this.titular = titular;
        this.saldo = depositoInicial;
    }

    internal void Deposito(double quantia)
    {
        this.saldo += quantia;
    }

    internal void Saque(double quantia)
    {
        this.saldo -= (quantia + taxaSaque);
    }
    public override string ToString()
    {
        var cultura = new CultureInfo("en-us");
        cultura.NumberFormat.NumberDecimalDigits = 2;

        return $"Conta {numero}, Titular: {titular}, Saldo: $ {string.Format(cultura, "{0:N}", saldo)}";
    }
}

using System.Drawing;
using System;
using System.Security.Cryptography.Xml;
using Questao5.Domain.Enumerators;

namespace Questao5.Domain.Entities;

public class Movimento
{
    public Guid IdMovimento { get; set; }
    public int IdContaCorrente { get; set; }
    public string DataMovimento { get; set; } = String.Empty;
    public TipoMovimento TipoMovimento { get; set; } 
    public decimal Valor { get; set; }

};
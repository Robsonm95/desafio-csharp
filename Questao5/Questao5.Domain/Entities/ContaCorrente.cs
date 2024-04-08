using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Questao5.Domain.Entities;

public class ContaCorrente
{
    public string IdContaCorrente { get; set; }
    public int Numero { get; set; }
    public string Nome{ get; set; }
    public bool Ativo { get; set; }
}

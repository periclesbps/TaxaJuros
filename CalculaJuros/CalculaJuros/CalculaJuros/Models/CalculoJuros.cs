using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculaJuros.Models
{
    public class CalculoJuros : RetornoBase
    {
        public decimal ValorFinal
        {
            get
            {
                return this.ValorInicial * Convert.ToDecimal(Math.Pow(1 + this.Juros, Tempo));
            }
        }
        public decimal ValorInicial { get; set; }
        public double Juros { get; set; }
        public int Tempo { get; set; }
    }
}

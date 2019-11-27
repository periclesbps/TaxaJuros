using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculaJuros.Models
{
    public class JurosCompostos : RetornoBase
    {
        public string ValorFinal
        {
            get
            {
                var calculo = Convert.ToDouble(this.ValorInicial) * Math.Pow(1 + this.Juros, Tempo);
                calculo = Math.Truncate(calculo * 100) / 100;

                return String.Format("{0:0.00}", calculo);
            }
        }
        public decimal ValorInicial { get; set; }
        public double Juros { get; set; }
        public int Tempo { get; set; }
    }
}

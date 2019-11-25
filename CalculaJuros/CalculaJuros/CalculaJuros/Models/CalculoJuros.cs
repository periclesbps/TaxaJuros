using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculaJuros.Models
{
    public class CalculoJuros : RetornoBase
    {
        public string ValorFinal
        {
            get
            {
                var calculo = Convert.ToDouble(this.ValorInicial) * Math.Pow(1 + this.Juros, Tempo);
                calculo = Math.Truncate(calculo * 100) / 100;

                var array = calculo.ToString().Split(",");
                if (array.Length > 1 && array[1].Length == 1)
                {
                    return string.Format("{0},{1}", array[0], array[1].PadRight(2, '0'));
                }

                return calculo.ToString();
            }
        }
        public decimal ValorInicial { get; set; }
        public double Juros { get; set; }
        public int Tempo { get; set; }
    }
}

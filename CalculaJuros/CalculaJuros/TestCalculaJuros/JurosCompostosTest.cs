using CalculaJuros.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestCalculaJuros
{
    [TestClass]
    public class JurosCompostosTest
    {
        [TestMethod]
        public void SeJurosIgualAZero_ValorFinalIgualAoInicial()
        {
            var juros = new JurosCompostos
            {
                ValorInicial = 100,
                Tempo = 5
            };

            Assert.AreEqual(Convert.ToDecimal(juros.ValorFinal), juros.ValorInicial);
        }

        [TestMethod]
        public void SeTempoZerado_ValorFinalIgualAoInicial()
        {
            var juros = new JurosCompostos
            {
                Juros = 0.01D,
                ValorInicial = 100,
            };

            Assert.AreEqual(Convert.ToDecimal(juros.ValorFinal), juros.ValorInicial);
        }

        [TestMethod]
        public void SeValorInicialZerado_ValorFinalZerado()
        {
            var juros = new JurosCompostos
            {
                Juros = 0.01D,
                Tempo = 5
            };

            Assert.AreEqual(Convert.ToDecimal(juros.ValorFinal), Convert.ToDecimal(0));
        }

        [TestMethod]
        public void RetornaCalculoCorreto()
        {
            var juros = new JurosCompostos
            {
                Juros = 0.01D,
                ValorInicial = 100,
                Tempo = 5
            };

            Assert.AreEqual(Convert.ToDecimal(juros.ValorFinal), Convert.ToDecimal(105.10D));
        }
    }
}

using CalculaJuros.Controllers;
using CalculaJuros.Models;
using CalculaJuros.Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculaJuros
{
    [TestClass]
    public class ControllerTest
    {
        private Mock<RgrCalcularJuros> MockRegra;
        private CalculaController Controller;

        [TestInitialize]
        public void Init()
        {
            this.MockRegra = new Mock<RgrCalcularJuros>();
            this.Controller = new CalculaController(this.MockRegra.Object);
        }

        [TestMethod]
        public void QuandoQuandoCalculoComErro_RetornaMsg()
        {
            this.MockRegra.Setup(x => x.Calcular(It.IsAny<decimal>(), It.IsAny<int>())).
                Returns(Task.FromResult(new JurosCompostos
                {
                    Mensagem = "Erro ao pesquisar"
                }));

            var resultado = this.Controller.Get(100, 5);

            Assert.AreEqual(resultado.Result.ToString(), "Erro ao pesquisar");
        }

        [TestMethod]
        public void QuandoCorreto_RetornaValorCalculo()
        {
            this.MockRegra.Setup(x => x.Calcular(It.IsAny<decimal>(), It.IsAny<int>())).
                Returns(Task.FromResult(new JurosCompostos
                {
                    Juros = 0.01D,
                    Tempo = 5,
                    ValorInicial = 100,
                    Sucesso = true
                }));

            var resultado = this.Controller.Get(100, 5);

            Assert.AreEqual(resultado.Result.ToString(), "105,10");
        }
    }
}

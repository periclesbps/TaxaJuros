using CalculaJuros;
using CalculaJuros.Models;
using CalculaJuros.Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestCalculaJuros
{
    [TestClass]
    public class RgrCalcularJurosTest
    {
        private RgrCalcularJuros Regra;
        private Mock<TaxaJurosRepositorio> MockRepositorio;
        private decimal ValorInicial;
        private int Meses;

        [TestInitialize]
        public void Init()
        {
            this.MockRepositorio = new Mock<TaxaJurosRepositorio>();
            this.Regra = new RgrCalcularJuros(this.MockRepositorio.Object);
            this.ValorInicial = 100;
            this.Meses = 5;
        }

        [TestMethod]
        public void QuandoValorInicialNaoInformado_RetornaFalso()
        {
            this.ValorInicial = 0;
            var retorno = this.Regra.Calcular(this.ValorInicial, this.Meses);

            Assert.IsFalse(retorno.Result.Sucesso);
            Assert.IsTrue(retorno.Result.Mensagem.Contains("Valor Inicial"));
            Assert.IsFalse(retorno.Result.Mensagem.Contains("Meses"));
        }

        [TestMethod]
        public void QuandoMesNaoInformado_RetornaErro()
        {
            this.Meses = -1;
            var retorno = this.Regra.Calcular(this.ValorInicial, this.Meses);

            Assert.IsFalse(retorno.Result.Sucesso);
            Assert.IsFalse(retorno.Result.Mensagem.Contains("Valor Inicial"));
            Assert.IsTrue(retorno.Result.Mensagem.Contains("Meses"));
        }

        [TestMethod]
        public void QuandoDadosRepositorioSemFuncionar_RetornarErro()
        {
            this.MockRepositorio.Setup(x => x.GetTaxa()).Returns(Task.FromResult(new Taxa()));
            var retorno = this.Regra.Calcular(this.ValorInicial, this.Meses);

            Assert.IsFalse(retorno.Result.Sucesso);
        }

        [TestMethod]
        public void QuandoDadosInformadosCorretamente_RetornarDados()
        {
            this.MockRepositorio.Setup(x => x.GetTaxa()).Returns(Task.FromResult(new Taxa { Valor = 0.01M, Sucesso = true }));
            var retorno = this.Regra.Calcular(this.ValorInicial, this.Meses);

            Assert.IsTrue(retorno.Result.Sucesso);
            Assert.AreEqual(retorno.Result.ValorFinal, "105,10");
        }
    }
}
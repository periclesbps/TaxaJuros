using CalculaJuros;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestCalculaJuros
{
    [TestClass]
    public class TaxaJurosRepositorioTest
    {
        private TaxaJurosRepositorio TaxaJurosRepositorio;

        [TestInitialize]
        public void Init()
        {
            this.TaxaJurosRepositorio = new TaxaJurosRepositorio();
        }

        [TestMethod]
        public void QuandoEnderecoInvalido_RetornaErro()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://www.teste.com/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            this.TaxaJurosRepositorio = new TaxaJurosRepositorio(client);

            var result = this.TaxaJurosRepositorio.GetTaxa();

            Assert.IsFalse(result.Result.Sucesso);
            Assert.AreEqual(result.Result.Mensagem, "Erro ao buscar a taxa de juros");
        }
    }
}

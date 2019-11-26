using CalculaJuros.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CalculaJuros
{
    public class TaxaJurosRepositorio
    {
        private HttpClient _client;
        private HttpClient Client
        {
            get
            {
                if (this._client == null)
                {
                    this._client = new HttpClient();
                    this._client.BaseAddress = new Uri("http://localhost:80/");
                    this._client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }

                return this._client;
            }
        }

        public TaxaJurosRepositorio()
        {
        }

        public TaxaJurosRepositorio(HttpClient c)
        {
            _client = c;
        }

        public virtual async Task<Taxa> GetTaxa()
        {
            var taxa = new Taxa();

            try
            {
                await this.BuscarDados(taxa);
            }
            catch (Exception ex)
            {
                taxa.Mensagem = ex.Message;
            }

            return taxa;
        }

        private async Task BuscarDados(Taxa taxa)
        {
            HttpResponseMessage response = await this.Client.GetAsync("taxajuros");

            if (response.IsSuccessStatusCode)
            {
                await this.Desserializar(taxa, response);

                taxa.Sucesso = true;
            }
            else
            {
                taxa.Mensagem = "Erro ao buscar a taxa de juros";
            }
        }

        private async Task Desserializar(Taxa taxa, HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();

            taxa.Valor = JsonConvert.DeserializeObject<decimal>(data, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
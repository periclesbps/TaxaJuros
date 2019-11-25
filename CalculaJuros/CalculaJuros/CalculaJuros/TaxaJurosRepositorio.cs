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
                    this._client.BaseAddress = new Uri("http://localhost:51677/");
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

        public async Task<Taxa> GetTaxa()
        {
            var taxa = new Taxa();

            try
            {
                HttpResponseMessage response = await this.Client.GetAsync("taxajuros");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();

                    taxa.Valor = JsonConvert.DeserializeObject<decimal>(data, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    taxa.Sucesso = true;
                }
            }
            catch (Exception ex)
            {
                taxa.Mensagem = ex.Message;
            }

            return taxa;
        }
    }
}
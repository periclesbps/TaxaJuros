using CalculaJuros.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CalculaJuros
{
    public class TaxaJurosRepositorio
    {
        HttpClient client = new HttpClient();

        public TaxaJurosRepositorio()
        {
            client.BaseAddress = new Uri("http://localhost:51677/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Taxa> GetTaxa()
        {
            var taxa = new Taxa();

            HttpResponseMessage response = await client.GetAsync("taxajuros");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                taxa = JsonConvert.DeserializeObject<Taxa>(data);
            }

            return taxa;
        }
    }
}

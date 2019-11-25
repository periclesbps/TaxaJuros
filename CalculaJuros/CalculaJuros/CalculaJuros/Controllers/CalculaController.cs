using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculaJuros.Models;
using CalculaJuros.Negocio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculaJuros.Controllers
{
    [Route("calculaJuros/")]
    public class CalculaController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<string> Get([FromQuery(Name = "valorinicial")] decimal valorInicial, [FromQuery(Name = "meses")] int meses)
        {
            var retorno = "";

            var regra = new RgrCalcularJuros();

            var calculo = await regra.Calcular(valorInicial, meses);

            if (calculo.Sucesso)
            {
                retorno = calculo.ValorFinal.ToString();
            }
            else
            {
                retorno = calculo.Mensagem;
            }

            return retorno;
        }

        //[HttpGet("{id}")]
        //public string Get([FromQuery(Name = "valorinicial")] decimal valorInicial, [FromQuery(Name = "meses")] int meses)
        //{
        //    var retorno = string.Empty;

        //    var regra = new RgrCalcularJuros();

        //    var calculoRealizado = regra.Calcular(valorInicial, meses);

        //    if (calculoRealizado.Sucesso)
        //    {
        //        retorno = calculoRealizado.ValorFinal.ToString();
        //    }
        //    else
        //    {
        //        retorno = calculoRealizado.Mensagem;
        //    }

        //    return retorno;
        //}

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

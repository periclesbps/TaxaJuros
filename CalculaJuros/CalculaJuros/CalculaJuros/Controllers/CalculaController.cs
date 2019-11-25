using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculaJuros.Negocio;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalculaJuros.Controllers
{
    [Route("calculaJuros/{valorinicial}/{meses}")]
    public class CalculaController : Controller
    {
        [HttpGet]
        public string Get([FromQuery]decimal valorInicial, [FromQuery]int meses)
        {
            var retorno = string.Empty;

            var regra = new RgrCalcularJuros();

            var calculoRealizado = regra.Calcular(valorInicial, meses);

            if (calculoRealizado.Sucesso)
            {
                retorno = calculoRealizado.ValorFinal.ToString();
            }
            else
            {
                retorno = calculoRealizado.Mensagem;
            }

            return retorno;
        }
    }
}

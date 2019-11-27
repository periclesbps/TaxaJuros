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
    [Route("calculajuros/")]
    public class CalculaController : Controller
    {
        //private RgrCalcularJuros _regra;
        //private RgrCalcularJuros Regra
        //{
        //    get
        //    {
        //        if (this._regra == null)
        //        {
        //            this._regra = new RgrCalcularJuros();
        //        }
        //        return this._regra;
        //    }
        //}

        //public CalculaController()
        //{

        //}

        //public CalculaController(RgrCalcularJuros rgr)
        //{
        //    this._regra = rgr;
        //}

        // GET: api/<controller>
        [HttpGet]
        public async Task<string> Get([FromQuery(Name = "valorinicial")] decimal valorInicial, [FromQuery(Name = "meses")] int meses)
        {
            var regra = new RgrCalcularJuros();

            var retorno = "";

            var calculo = await regra.Calcular(valorInicial, meses);

            if (calculo.Sucesso)
            {
                retorno = calculo.ValorFinal;
            }
            else
            {
                retorno = calculo.Mensagem;
            }

            return retorno;
        }
    }
}
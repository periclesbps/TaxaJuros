using CalculaJuros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculaJuros.Negocio
{
    public class RgrCalcularJuros : RetornoBase
    {
        private TaxaJurosRepositorio _ws;
        private TaxaJurosRepositorio WS
        {
            get
            {
                if (this._ws == null)
                {
                    this._ws = new TaxaJurosRepositorio();
                }

                return this._ws;
            }
        }

        public RgrCalcularJuros()
        {

        }

        public RgrCalcularJuros(TaxaJurosRepositorio ws)
        {
            this._ws = ws;
        }

        public CalculoJuros Calcular(decimal valorInicial, int tempo)
        {
            var retorno = new CalculoJuros();

            try
            {
                retorno.Mensagem = this.ValidaDadosEntrada(valorInicial, tempo);

                if (!string.IsNullOrEmpty(retorno.Mensagem))
                    return retorno;

                var taxa = this.ObterTaxaJuros();

                if (!taxa.Sucesso)
                {
                    retorno.Mensagem = taxa.Mensagem;
                    return retorno;
                }

                retorno.Juros = Convert.ToDouble(taxa.Valor);
                retorno.ValorInicial = valorInicial;
                retorno.Tempo = tempo;
            }
            catch (Exception ex)
            {
                retorno.Mensagem = ex.Message;
            }

            return retorno;
        }

        private string ValidaDadosEntrada(decimal valorInicial, int tempo)
        {
            var retorno = string.Empty;
            var erros = new List<string>();

            if (valorInicial < 1)
            {
                erros.Add("Valor Inicial");
            }

            if (tempo < 1)
            {
                erros.Add("Tempo");
            }

            if (erros.Any())
            {
                retorno = string.Format("Os campos informados são inválidos: {0}", string.Join(",", erros));
            }

            return retorno;
        }

        private Taxa ObterTaxaJuros()
        {
            var retorno = new Taxa();

            var task = this.WS.GetTaxa();

            task.ContinueWith(task =>
            {
                retorno = task.Result;
            },
            TaskContinuationOptions.OnlyOnRanToCompletion);

            return retorno;
        }
    }
}
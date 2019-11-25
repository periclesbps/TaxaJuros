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
        private TaxaJurosRepositorio _repositorio;
        private TaxaJurosRepositorio Repositorio
        {
            get
            {
                if (this._repositorio == null)
                {
                    this._repositorio = new TaxaJurosRepositorio();
                }

                return this._repositorio;
            }
        }

        public RgrCalcularJuros()
        {

        }

        public RgrCalcularJuros(TaxaJurosRepositorio ws)
        {
            this._repositorio = ws;
        }

        public async Task<CalculoJuros> Calcular(decimal valorInicial, int tempo)
        {
            var retorno = new CalculoJuros();

            try
            {
                retorno.Mensagem = this.ValidaDadosEntrada(valorInicial, tempo);

                if (!string.IsNullOrEmpty(retorno.Mensagem))
                    return retorno;

                var taxa = await this.ObterTaxaJuros();

                //taxaTask.ContinueWith(task =>
                //{
                //    retorno = task.Result;
                //},
                //TaskContinuationOptions.OnlyOnRanToCompletion);


                if (!taxa.Sucesso)
                {
                    retorno.Mensagem = taxa.Mensagem;
                    return retorno;
                }

                retorno.Juros = Convert.ToDouble(taxa.Valor);
                retorno.ValorInicial = valorInicial;
                retorno.Tempo = tempo;
                retorno.Sucesso = true;
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

        private async Task<Taxa> ObterTaxaJuros()
        {
            //var retorno = new Taxa();


            return await this.Repositorio.GetTaxa();

            //taxaTask.ContinueWith(task =>
            //{
            //    retorno = task.Result;
            //},
            //TaskContinuationOptions.OnlyOnRanToCompletion);

            //return retorno;
        }
    }
}
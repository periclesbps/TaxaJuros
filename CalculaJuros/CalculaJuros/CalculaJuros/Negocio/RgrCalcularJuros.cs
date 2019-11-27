using CalculaJuros.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculaJuros.Negocio
{
    public class RgrCalcularJuros
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

        public virtual async Task<JurosCompostos> Calcular(decimal valorInicial, int tempo)
        {
            var retorno = new JurosCompostos();

            try
            {
                retorno.Mensagem = this.ValidaDadosEntrada(valorInicial, tempo);

                if (!string.IsNullOrEmpty(retorno.Mensagem))
                {
                    return retorno;
                }

                var taxaJuros = await this.Repositorio.GetTaxa();

                if (!taxaJuros.Sucesso)
                {
                    retorno.Mensagem = taxaJuros.Mensagem;
                    return retorno;
                }

                retorno = this.DefinirValoresParaCalculo(valorInicial, tempo, taxaJuros.Valor);
            }
            catch (Exception ex)
            {
                retorno.Mensagem = ex.Message;
            }

            return retorno;
        }

        private JurosCompostos DefinirValoresParaCalculo(decimal valorInicial, int tempo, decimal juros)
        {
            return new JurosCompostos
            {
                Juros = Convert.ToDouble(juros),
                ValorInicial = valorInicial,
                Tempo = tempo,
                Sucesso = true
            };
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
                erros.Add("Meses");
            }

            if (erros.Any())
            {
                retorno = string.Format("Os campos informados são inválidos: {0}", string.Join(",", erros));
            }

            return retorno;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace InformaticaPecas.ViewModels
{
    public class ParametrosPaginacao
    {
        public ParametrosPaginacao(NameValueCollection dados)
        {
            string chave = dados.AllKeys.Where(k => k.StartsWith("sort")).First();
            string ordenacao = dados[chave];
            string campo = chave.Replace("sort[", String.Empty).Replace("]", String.Empty);

            CampoOrdenado = String.Format("{0} {1}", campo, ordenacao);

            Current = int.Parse(dados["current"]);
            RowCount = int.Parse(dados["rowCount"]);
            SearchPhrase = dados["searchPhrase"];
        }

        public int Current { get; set; }
        public int RowCount { get; set; }
        public string SearchPhrase { get; set; }
        public string CampoOrdenado { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformaticaPecas.ViewModels
{
    public class DadosFiltrados
    {
        public DadosFiltrados(ParametrosPaginacao parametrosPaginacao)
        {
            rowCount = parametrosPaginacao.RowCount;
            current = parametrosPaginacao.Current;
        }
        public dynamic rows { get; set; }
        public int current { get; set; }
        public int rowCount { get; set; }
        public int total { get; set; }
    }
}
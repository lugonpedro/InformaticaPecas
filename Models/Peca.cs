using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InformaticaPecas.Models
{
    public class Peca
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public decimal Valor { get; set; }

        public Tipo Tipo { get; set; }
        public int TipoId { get; set; }

        public Fornecedor Fornecedor { get; set; }
        public int FornecedorId { get; set; }
    }
}
using InformaticaPecas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace InformaticaPecas.AcessoDados
{
    public class PecaContexto : DbContext
    {
        public DbSet<Fornecedor> Fornecedor { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Tipo> Tipos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(100));
        }
    }
}
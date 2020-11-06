using InformaticaPecas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InformaticaPecas.AcessoDados
{
    public class PecasInit : CreateDatabaseIfNotExists<PecaContexto>
    {
        protected override void Seed(PecaContexto context)
        {
            List<Fornecedor> fornecedors = new List<Fornecedor>()
            {
                new Fornecedor() { Nome = "Intel" },
                new Fornecedor() { Nome = "AMD" },
                new Fornecedor() { Nome = "NVIDIA" },
                new Fornecedor() { Nome = "HyperX" },
                new Fornecedor() { Nome = "ASUS" },
                new Fornecedor() { Nome = "Cooler Master" },
            };

            fornecedors.ForEach(f => context.Fornecedor.Add(f));

            List<Tipo> tipos = new List<Tipo>()
            {
                new Tipo() { Nome = "RAM" },
                new Tipo() { Nome = "Placa de Video" },
                new Tipo() { Nome = "Placa Mae" },
                new Tipo() { Nome = "Processador" },
                new Tipo() { Nome = "Gabinete" },
            };

            tipos.ForEach(t => context.Tipos.Add(t));

            List<Peca> pecas = new List<Peca>()
            {
                new Peca()
                {
                    Produto = "4GB DDR3",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "RAM"),
                    Valor = 150,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "HyperX")
                },
                new Peca()
                {
                    Produto = "8GB DDR3",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "RAM"),
                    Valor = 200,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "HyperX")
                },
                new Peca()
                {
                    Produto = "RTX 2070",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Placa de Video"),
                    Valor = 3000,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "AMD")
                },
                new Peca()
                {
                    Produto = "GTX 1050 Ti",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Placa de Video"),
                    Valor = 1000,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "NVIDIA")
                },
                new Peca()
                {
                    Produto = "H310M",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Placa Mae"),
                    Valor = 500,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "ASUS")
                },
                new Peca()
                {
                    Produto = "A320M",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Placa Mae"),
                    Valor = 550,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "ASUS")
                },
                new Peca()
                {
                    Produto = "i3-9100",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Processador"),
                    Valor = 500,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "Intel")
                },
                new Peca()
                {
                    Produto = "Ryzen 5 1600",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Processador"),
                    Valor = 800,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "AMD")
                },
                new Peca()
                {
                    Produto = "MB500",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Gabinete"),
                    Valor = 0,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "Cooler Master")
                },
                new Peca()
                {
                    Produto = "H500P",
                    Tipo = tipos.FirstOrDefault(t => t.Nome == "Gabinete"),
                    Valor = 0,
                    Fornecedor = fornecedors.FirstOrDefault(f => f.Nome == "Cooler Master")
                },
            };

            pecas.ForEach(p => context.Pecas.Add(p));

            context.SaveChanges();
        }
    }
}
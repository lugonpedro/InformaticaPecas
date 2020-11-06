using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InformaticaPecas.AcessoDados;
using InformaticaPecas.Models;
using InformaticaPecas.ViewModels;
using Microsoft.Ajax.Utilities;

namespace InformaticaPecas.Controllers
{
    public class PecasController : Controller
    {   
        private PecaContexto db = new PecaContexto();

        // GET: Pecas
        public ActionResult Index()
        {
            var pecas = db.Pecas.Include(p => p.Fornecedor);
            return View();
        }
        public ActionResult GraficoPPF()
        {
            ViewBag.Labels = NomearFornecedores();
            ViewBag.Data = ContarPecasPorFornecedor();

            return View();
        }
        
        public List<string> NomearFornecedores()
        {
            var fornecedores = new List<string>();
            foreach (Fornecedor f in db.Fornecedor)
            {
                fornecedores.Add(f.Nome);
            }
            return fornecedores;
        }

        public int PecasPorFornecedor(int fornecedor)
        {
            var pecas = db.Pecas.Where($"FornecedorId == {fornecedor}");
            int pecasContadas = pecas.Count();
            return pecasContadas;
        }

        public int FornecedoresContados()
        {
            var fornecedores = db.Fornecedor.Count();
            return fornecedores;
        }

        public List<int> ContarPecasPorFornecedor()
        {
            var nDePecas = new List<int>();
            for(int i=0; i < FornecedoresContados(); i++)
            {
                nDePecas.Add(PecasPorFornecedor(i + 1));
            }
            return nDePecas;
        }
        public ActionResult GraficoPPT()
        {
            ViewBag.Labels = NomearTipos();
            ViewBag.Data = ContarPecasPorTipo();

            return View();
        }
        public List<string> NomearTipos()
        {
            var tipos = new List<string>();
            foreach (Tipo t in db.Tipos)
            {
                tipos.Add(t.Nome);
            }
            return tipos;
        }
        public int PecasPorTipo(int tipo)
        {
            var pecas = db.Pecas.Where($"TipoId == {tipo}");
            int pecasContadas = pecas.Count();
            return pecasContadas;
        }
        public int TiposContados()
        {
            var tipos = db.Tipos.Count();
            return tipos;
        }
        public List<int> ContarPecasPorTipo()
        {
            var nDePecas = new List<int>();
            for (int i = 0; i < TiposContados(); i++)
            {
                nDePecas.Add(PecasPorTipo(i + 1));
            }
            return nDePecas;
        }
        public JsonResult Listar(ParametrosPaginacao parametrosPaginacao)
        {
            DadosFiltrados dadosFiltrados = FiltrarEPaginar(parametrosPaginacao);

            return Json(dadosFiltrados, JsonRequestBehavior.AllowGet);
        }

        private DadosFiltrados FiltrarEPaginar(ParametrosPaginacao parametrosPaginacao)
        {
            var pecas = db.Pecas.Include(p => p.Fornecedor);

            int total = pecas.Count();

            if (!String.IsNullOrWhiteSpace(parametrosPaginacao.SearchPhrase))
            {
                decimal valor = 0;
                decimal.TryParse(parametrosPaginacao.SearchPhrase, out valor);

                pecas = pecas.Where("Produto.Contains(@0) OR Valor = @1", parametrosPaginacao.SearchPhrase, valor);
            }

            var pecasPaginadas = pecas.OrderBy(parametrosPaginacao.CampoOrdenado).Skip((parametrosPaginacao.Current - 1) * parametrosPaginacao.RowCount).Take(parametrosPaginacao.RowCount);

            DadosFiltrados dadosFiltrados = new DadosFiltrados(parametrosPaginacao)
            {
                rows = pecasPaginadas.ToList(),
                total = total
            };
            return dadosFiltrados;
        }

        // GET: Pecas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peca peca = db.Pecas.Find(id);
            if (peca == null)
            {
                return HttpNotFound();
            }

            return PartialView(peca);
        }

        // GET: Pecas/Create
        public ActionResult Create()
        {
            ViewBag.FornecedorId = new SelectList(db.Fornecedor, "Id", "Nome");
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome");
            return PartialView();
        }

        // POST: Pecas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create([Bind(Include = "Id,Produto,Valor,TipoId,FornecedorId")] Peca peca)
        {
            if (ModelState.IsValid)
            {
                db.Pecas.Add(peca);
                db.SaveChanges();
                return Json(new { resultado = true, mensagem = "Peca cadastrada com sucesso"});
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                return Json(new { resultado = false, mensagem = erros });
            }
        }

        // GET: Pecas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peca peca = db.Pecas.Find(id);
            if (peca == null)
            {
                return HttpNotFound();
            }
            ViewBag.FornecedorId = new SelectList(db.Fornecedor, "Id", "Nome", peca.FornecedorId);
            ViewBag.TipoId = new SelectList(db.Tipos, "Id", "Nome", peca.TipoId);
            return PartialView(peca);
        }

        // POST: Pecas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit([Bind(Include = "Id,Produto,Valor,TipoId,FornecedorId")] Peca peca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(peca).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { resultado = true, mensagem = "Peca editada com sucesso" });
            }
            else
            {
                IEnumerable<ModelError> erros = ModelState.Values.SelectMany(item => item.Errors);
                return Json(new { resultado = false, mensagem = erros });
            }

        }

        // GET: Pecas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Peca peca = db.Pecas.Find(id);
            if (peca == null)
            {
                return HttpNotFound();
            }
            return PartialView(peca);
        }

        // POST: Pecas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Peca peca = db.Pecas.Find(id);
                db.Pecas.Remove(peca);
                db.SaveChanges();
                return Json(new { resultado = true, mensagem = "Peca excluida com sucesso" });
            }
            catch(Exception ex)
            {
                return Json(new { resultado = false, mensagem = ex.Message });
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

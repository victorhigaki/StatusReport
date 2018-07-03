using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StatusReportOficial.Models;
using StatusReportOficial.Context;

namespace StatusReportOficial.Controllers
{
    public class ProjetosController : Controller
    {
        private StatusReportOficialContext db = new StatusReportOficialContext();

        // GET: /Projetos/
        public ActionResult Index()
        {
            return View(db.Projetos.ToList());
        }

        // GET: /Projetos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projetos projetos = db.Projetos.Find(id);
            if (projetos == null)
            {
                return HttpNotFound();
            }
            return View(projetos);
        }

        // GET: /Projetos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Projetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,nomeProjeto,cliente,dataInicio,dataFim")] Projetos projetos)
        {
            if (ModelState.IsValid)
            {
                db.Projetos.Add(projetos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projetos);
        }

        // GET: /Projetos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projetos projetos = db.Projetos.Find(id);
            if (projetos == null)
            {
                return HttpNotFound();
            }
            return View(projetos);
        }

        // POST: /Projetos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,nomeProjeto,cliente,dataInicio,dataFim")] Projetos projetos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projetos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projetos);
        }

        // GET: /Projetos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projetos projetos = db.Projetos.Find(id);
            if (projetos == null)
            {
                return HttpNotFound();
            }
            return View(projetos);
        }

        // POST: /Projetos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projetos projetos = db.Projetos.Find(id);
            db.Projetos.Remove(projetos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // POST: /Projetos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CadastrarProjeto([Bind(Prefix="item1")] Projetos projeto)
        {
            var usuario_id = Convert.ToInt32(Session["id"]);

            var usuarioBD = db.Usuarios.Where(u => u.Id.Equals(usuario_id)).FirstOrDefault();
            
                if (ModelState.IsValid)
                {
                    usuarioBD.Projetos.Add(projeto);

                   // db.Projetos.Add(projetos);
                    db.SaveChanges();

                   return RedirectToAction("Home", "Home");
                }
            return View(projeto);
        }

        // GET: /Projetos/
        public ActionResult ProjetoDetalhes(int ?id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Projetos projeto = db.Projetos.Find(id);

                List<int> id_Sprints_projeto = new List<int>();

                //Já é possível criar os botoes das sprints com a lista 
                //id_Sprints_projeto que contem os ids das sprints pertencentes ao projeto especifico
                foreach (var sprints in projeto.Sprints)
                {
                    id_Sprints_projeto.Add(sprints.Id);
                }
                  
                var tupla = Tuple.Create<Projetos, IEnumerable<int>, Sprints>(projeto, id_Sprints_projeto, new Sprints());

                return View(tupla);
            }
        }



        public ActionResult ProjetoSprint(int? idProjeto, int? idSprint)
        {
            Projetos projeto = db.Projetos.Find(idProjeto);
            //Passar como parametro o id do botão equivalente a Sprint que foi gerado dinamicamente 
            var sprint = db.Sprints.Find(idSprint);

            List<BurnDown> lista_burnDown = sprint.BurnDown.ToList();

            //var lista_burnDown = db.BurnDowns.ToList();

            var tupla = Tuple.Create<Projetos, BurnDown ,IEnumerable<BurnDown>, Sprints>(projeto, new BurnDown(), lista_burnDown, sprint);

            return View(tupla);
        }

    }
}

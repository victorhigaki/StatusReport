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
    public class SprintsController : Controller
    {
        private StatusReportOficialContext db = new StatusReportOficialContext();

        // GET: /Sprints/
        public ActionResult Index()
        {
            return View(db.Sprints.ToList());
        }

        // GET: /Sprints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprints sprints = db.Sprints.Find(id);
            if (sprints == null)
            {
                return HttpNotFound();
            }
            return View(sprints);
        }

        // GET: /Sprints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Sprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,qtd_tarefas,dias")] Sprints sprints)
        {
            if (ModelState.IsValid)
            {
                db.Sprints.Add(sprints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sprints);
        }

        // GET: /Sprints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprints sprints = db.Sprints.Find(id);
            if (sprints == null)
            {
                return HttpNotFound();
            }
            return View(sprints);
        }

        // POST: /Sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,qtd_tarefas,dias")] Sprints sprints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sprints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sprints);
        }

        // GET: /Sprints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprints sprints = db.Sprints.Find(id);
            if (sprints == null)
            {
                return HttpNotFound();
            }
            return View(sprints);
        }

        // POST: /Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprints sprints = db.Sprints.Find(id);
            db.Sprints.Remove(sprints);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarSprint([Bind(Prefix = "item3")] Sprints sprint)
        {

            int idProjeto = Convert.ToInt32(Request.QueryString["idProjeto"]);

            var ProjetoBD = db.Projetos.Where(p => p.Id.Equals(idProjeto)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                ProjetoBD.Sprints.Add(sprint);

                db.SaveChanges();

                return RedirectToAction("ProjetoDetalhes", "Projetos", new { id = idProjeto });
            }
            return View(ProjetoBD);
        }

    }
}

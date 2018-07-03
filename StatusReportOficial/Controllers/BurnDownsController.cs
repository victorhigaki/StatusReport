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
    public class BurnDownsController : Controller
    {
        private StatusReportOficialContext db = new StatusReportOficialContext();

        // GET: /BurnDowns/
        public ActionResult Index()
        {
            return View(db.BurnDowns.ToList());
        }

        // GET: /BurnDowns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BurnDown burndown = db.BurnDowns.Find(id);
            if (burndown == null)
            {
                return HttpNotFound();
            }
            return View(burndown);
        }

        // GET: /BurnDowns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /BurnDowns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dia_sprint,qtd_reazalidas")] BurnDown burndown)
        {
            if (ModelState.IsValid)
            {
                db.BurnDowns.Add(burndown);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(burndown);
        }

        // GET: /BurnDowns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BurnDown burndown = db.BurnDowns.Find(id);
            if (burndown == null)
            {
                return HttpNotFound();
            }
            return View(burndown);
        }

        // POST: /BurnDowns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dia_sprint,qtd_reazalidas")] BurnDown burndown)
        {
            if (ModelState.IsValid)
            {
                db.Entry(burndown).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(burndown);
        }

        // GET: /BurnDowns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BurnDown burndown = db.BurnDowns.Find(id);
            if (burndown == null)
            {
                return HttpNotFound();
            }
            return View(burndown);
        }

        // POST: /BurnDowns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BurnDown burndown = db.BurnDowns.Find(id);
            db.BurnDowns.Remove(burndown);
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
        public ActionResult CadastrarBurnDown([Bind(Prefix = "item2")] BurnDown burnDown)
        {

            //var teste = HttpContext.Request.Params.Get(1);

            int idProjeto = Convert.ToInt32(Request.QueryString["idProjeto"]);

            int idSprint = Convert.ToInt32(Request.QueryString["idSprint"]);

         
            //var teste2 = RouteData.Values["idSprint"] + Request.Url.Query;

            var SprintBD = db.Sprints.Where(s => s.Id.Equals(idSprint)).FirstOrDefault();

            if (ModelState.IsValid)
            {
                SprintBD.BurnDown.Add(burnDown);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }

                return RedirectToAction("ProjetoSprint", "Projetos", new { idProjeto, idSprint });
            }
            return View(SprintBD);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AtualizarBurnDown([Bind(Prefix = "item2")] BurnDown burnDown)
        {

            //var teste = HttpContext.Request.Params.Get(1);

            int idProjeto = Convert.ToInt32(Request.QueryString["idProjeto"]);

            int idSprint = Convert.ToInt32(Request.QueryString["idSprint"]);

            var BurndownBD = db.BurnDowns.Where(k => k.FK_Id_Sprint.Id == idSprint && k.dia_sprint.Equals(burnDown.dia_sprint)).FirstOrDefault();

            BurndownBD.qtd_reazalidas = burnDown.qtd_reazalidas;

            if (ModelState.IsValid)
            {
                db.Entry(BurndownBD).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ProjetoSprint", "Projetos", new { idProjeto, idSprint });
            }
            return View(BurndownBD);
        }

    }

}

using StatusReportOficial.Context;
using StatusReportOficial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatusReportOficial.Controllers
{
    public class HomeController : Controller
    {

        private StatusReportOficialContext db = new StatusReportOficialContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Home()
        {
            var usuario_id = Convert.ToInt32(Session["id"]);

            var lista = db.Usuarios
                      .Where(c => c.Id == usuario_id)
                      .SelectMany(c => c.Projetos).ToList();

            var tupla = Tuple.Create<Projetos, IEnumerable<Projetos>>(new Projetos(), lista);
            return View(tupla);
        }


        public ActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Home");
            //}
            if (Session["id"] != null)
            {
                return RedirectToAction("Home");
            }
            return View();
        }
       
    }
}
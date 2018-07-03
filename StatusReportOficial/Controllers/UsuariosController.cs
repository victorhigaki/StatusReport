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
using System.Web.Security;

namespace StatusReportOficial.Controllers
{
    public class UsuariosController : Controller
    {
        private StatusReportOficialContext db = new StatusReportOficialContext();

        // GET: /Usuarios/
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: /Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: /Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Nome,Sobrenome,Login,Senha,Email,Adm,ativo")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: /Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: /Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Nome,Sobrenome,Login,Senha,Email,Adm,ativo")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: /Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: /Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
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

        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult Login(string returnURL)
        {
            /*Recebe a url que o usuário tentou acessar*/
            ViewBag.ReturnUrl = returnURL;
            return View(new Usuarios());
        }

        /// <param name="login"></param>
        /// <param name="returnUrl"></param>    
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios loginAcesso, string returnUrl)
        {

            if (Session["id"] != null)
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                using (StatusReportOficialContext db = new StatusReportOficialContext())
                {
                    var vLogin = db.Usuarios.Where(p => p.Login.Equals(loginAcesso.Login)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. 
                    Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                          ativo. Caso não esteja cai direto no else*/
                        if (vLogin.ativo == true)
                        {
                            /*Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada 
                             do banco. Caso não cai direto no else*/
                            if (Equals(vLogin.Senha, loginAcesso.Senha))
                            {
                                FormsAuthentication.SetAuthCookie(vLogin.Login, false);
                                if (Url.IsLocalUrl(returnUrl)
                                && returnUrl.Length > 1
                                && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//")
                                && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                /*código abaixo cria uma session para armazenar o nome do usuário*/
                                Session["Nome"] = vLogin.Nome;
                                /*código abaixo cria uma session para armazenar o sobrenome do usuário*/
                                Session["Sobrenome"] = vLogin.Sobrenome;

                                /*código abaixo cria uma session para armazenar o id do usuário*/
                                Session["id"] = vLogin.Id;

                                /*retorna para a tela inicial do Home*/
                                return RedirectToAction("Home", "Home");
                            }
                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                //      ModelState.AddModelError("", "Senha informada Inválida!!!");
                                /*Retorna a tela de login*/
                                return View(new Usuarios());
                            }
                        }
                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*escreve na tela a mensagem de erro informada*/

                            ModelState.AddModelError("", "usuário sem acesso para usar o sistema!!!");
                            /*retorna a tela de login*/
                            return RedirectToAction("Login", "Home");
                        }
                    }
                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        // ModelState.AddModelError("", "Login informado inválido!!!");
                        /*Retorna a tela de login*/
                        return View(new Usuarios());
                    }
                }
            }
        }

        public ActionResult Logoff()
        {
            Session.Abandon();
            return RedirectToAction("Login","Home");
        }


    }
}

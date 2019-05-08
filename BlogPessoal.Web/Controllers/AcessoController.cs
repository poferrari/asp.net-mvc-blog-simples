using BlogPessoal.Web.Data.Contexto;
using BlogPessoal.Web.Models.Acesso;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogPessoal.Web.Controllers
{
    [Authorize]
    public class AcessoController : Controller
    {
        private BlogPessoalContexto db = new BlogPessoalContexto();

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            if (Request.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            if (!string.IsNullOrEmpty(returnUrl))
                TempData["ReturnUrl"] = returnUrl;
            var cookie = Request.Cookies["usuarioBlogPessoal"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                return View(new Login { Email = cookie.Value });
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Entrar(Login usuario)
        {
            if (!ModelState.IsValid)
                return View("Index");

            var autorLogado = db.Autores.Where(t => t.Email.Equals(usuario.Email, StringComparison.OrdinalIgnoreCase) &&
                    t.Senha.Equals(usuario.Senha)).FirstOrDefault();
            if (autorLogado != null)
            {
                var roles = autorLogado.Administrador ? "Admin" : "User";

                var authTicket = new FormsAuthenticationTicket(
                     1,
                     autorLogado.Email,
                     DateTime.Now,
                     DateTime.Now.AddDays(1),
                     false,
                     roles,
                     "/");
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);

                //FormsAuthentication.SetAuthCookie(autorLogado.Email, true);
                //AdicionarCookieLogin(autorLogado.Email);
                return RedirectToLocal();
            }
            return View("Index");
        }

        private ActionResult RedirectToLocal()
        {
            if (TempData["ReturnUrl"] != null)
            {
                var returnUrl = TempData["ReturnUrl"] as string;
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        private void AdicionarCookieLogin(string email)
        {
            HttpCookie cookie = Response.Cookies["usuarioBlogPessoal"];
            cookie.Value = email;
            cookie.Expires = DateTime.Today.AddDays(7);
        }
    }
}
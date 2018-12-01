using BlogPessoal.Web.Data.Contexto;
using BlogPessoal.Web.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogPessoal.Web.Controllers
{
    public class HomeController : Controller
    {
        private BlogPessoalContexto db = new BlogPessoalContexto();

        // GET: Home
        //[OutputCache(Duration = 10, VaryByParam = "none")]
        //[WhitespaceFilter]
        //[CompressFilter]
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CategoriasDeArtigo()
        {
            var lista = db.CategoriasDeArtigo.ToList();
            return PartialView("../Shared/_Categorias", lista);
        }
    }
}
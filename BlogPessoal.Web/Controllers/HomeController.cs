using BlogPessoal.Web.Data.Contexto;
using BlogPessoal.Web.Filtros;
using System.Linq;
using System.Web.Mvc;

namespace BlogPessoal.Web.Controllers
{
    public class HomeController : Controller
    {
        private BlogPessoalContexto db = new BlogPessoalContexto();
        
        //[OutputCache(Duration = 10, VaryByParam = "none")] //Cache
        //[WhitespaceFilter] //Remover espaços em branco
        //[CompressFilter] //Compressão
        [ExibirArtigosActionFilter]
        public ActionResult Index()
        {            
            ViewBag.UltimosArtigos = db.Artigos
                .OrderByDescending(t => t.DataPublicacao)
                .Take(5)
                .ToList();

            return View();
        }

        public ActionResult CategoriasDeArtigo()
        {
            var lista = db.CategoriasDeArtigo.ToList();
            return PartialView("../Shared/_Categorias", lista);
        }
    }
}
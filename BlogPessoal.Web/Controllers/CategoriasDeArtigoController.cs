using BlogPessoal.Web.Data.Contexto;
using System.Linq;
using System.Web.Mvc;

namespace BlogPessoal.Web.Controllers
{
    public class CategoriasDeArtigoController : Controller
    {
        public ActionResult Index()
        {
            using (var ctx = new BlogPessoalContexto())
            {
                var categorias = ctx.CategoriaDeArtigo.ToList();
            }
            return View();
        }
    }
}
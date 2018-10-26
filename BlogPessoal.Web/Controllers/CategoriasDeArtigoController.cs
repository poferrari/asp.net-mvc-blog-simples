using BlogPessoal.Web.Data.Contexto;
using System.Linq;
using System.Web.Mvc;

namespace BlogPessoal.Web.Controllers
{
    public class CategoriasDeArtigoController : Controller
    {
        public ActionResult Index()
        {            
            return View();
        }
    }
}
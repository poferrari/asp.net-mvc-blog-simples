using System.Web.Mvc;
using System.Web.Routing;

namespace BlogPessoal.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            //routes.MapRoute(
            //    name: "UrlAmigavelDetalhesArtigo",
            //    url: "Artigo/{ano}/{mes}/{dia}/{nome}/{id}",
            //    defaults: new { controller = "Artigos", action = "Detalhes", nome = (string)null },
            //    constraints: new { id = @"\d+" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

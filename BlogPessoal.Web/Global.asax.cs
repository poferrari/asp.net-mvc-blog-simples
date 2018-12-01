using BlogPessoal.Web.App_Start;
using System;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BlogPessoal.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BundleTable.EnableOptimizations = false;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            CompressaoDeRequisicao(app);
        }

        private void CompressaoDeRequisicao(HttpApplication app)
        {
            string encodings = app.Request.Headers.Get("Accept-Encoding");
            if (encodings != null)
            {
                encodings = encodings.ToLower();
                if (encodings.Contains("deflate"))
                {
                    app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
                    app.Response.AppendHeader("Content-Encoding", "deflate");
                }
                else if (encodings.Contains("gzip"))
                {
                    app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
                    app.Response.AppendHeader("Content-Encoding", "gzip");
                }
            }
        }
    }
}

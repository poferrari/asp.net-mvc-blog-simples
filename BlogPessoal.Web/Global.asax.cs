using BlogPessoal.Web.App_Start;
using Rollbar;
using System;
using System.IO.Compression;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

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
            
            const string postServerItemAccessToken = "";
            RollbarLocator.RollbarInstance
                .Configure(new RollbarConfig(postServerItemAccessToken) { Environment = "producao" });
            
            MvcHandler.DisableMvcResponseHeader = true;//Remover o cabeçalho X-AspNetMvc-Version
        }

        /// <summary>
        /// Recriando o usuário com as roles, com base no conteúdo do cookie de autenticação
        /// </summary>        
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return;
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var roles = authTicket.UserData.Split(",".ToCharArray());
            var userPrincipal = new GenericPrincipal(new GenericIdentity(authTicket.Name), roles);
            Context.User = userPrincipal;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            CompressaoDeRequisicao(app);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //if (Request.Url.Host.ToLower().Contains("localhost"))
            //    return;
            var exception = Server.GetLastError().GetBaseException();
            RollbarLocator.RollbarInstance.Log(ErrorLevel.Error, exception);
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
   
        //Remover a versão do X-AspNet do cabeçalho do servidor
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");           //Remove Server Header   
            Response.Headers.Remove("X-AspNet-Version"); //Remove X-AspNet-Version Header
        }
    }
}

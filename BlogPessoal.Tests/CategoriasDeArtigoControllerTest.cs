using BlogPessoal.Web.Controllers;
using BlogPessoal.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace BlogPessoal.Tests
{
    [TestClass]
    public class CategoriasDeArtigoControllerTest
    {
        [TestMethod]
        public void Acessar_Details_com_sucesso()
        {
            var controller = new CategoriasDeArtigoController();

            var result = controller.Details(2) as ViewResult;
            var categoria = (CategoriaDeArtigo)result.ViewData.Model;

            Assert.IsNotNull(categoria);
        }
    }
}
